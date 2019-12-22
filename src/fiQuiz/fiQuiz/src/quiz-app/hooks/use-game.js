import React, { useState, useEffect, useContext, createContext } from "react";
import quizGameService, { AnswerOption, QuizJoker } from "../services/QuizGameService";
import { useLocalStorage } from "./use-local-storage";
import { useAuth } from "./use-auth";

const gameContext = createContext();

export function ProvideGame({ children }) {
    const game = useProvideGame();
    game.init();
    return <gameContext.Provider value={game}>{children}</gameContext.Provider>;
}

export const useGame = function () {
    return useContext(gameContext);
}

function useProvideGame() {
    const [step, setStep] = useLocalStorage("quizGameStep", "home");
    const [quizId, setQuizId] = useLocalStorage("quizGameQuizId", 0);
    const [quizQuestionId, setQuizQuestionId] = useLocalStorage("quizGameQuizQuestionId", 0);
    const [quizQuestionNumber, setQuizQuestionNumber] = useLocalStorage("quizGameQuizQuestionNumber", 0);
    const [quizQuestion, setQuizQuestion] = useLocalStorage("quizGameQuizQuestion", null);
    const [prevQuizQuestionId, setPrevQuizQuestionId] = useLocalStorage("quizGamePrevQuizQuestionId", 0);
    const [jokers, setJokers] = useLocalStorage("quizGameJokers", null);
    const [remainingTime, setRemainingTime] = useLocalStorage("quizGameRemainingTime", 0);
    const [timerStatus, setTimerStatus] = useLocalStorage("quizGameTimerStatus", false);
    const [userQuizList, setUserQuizList] = useState([]);
    const [userQuizListType, setUserQuizListType] = useLocalStorage("quizGameUserQuizListType", null);
    const [initFlag, setInitFlag] = useState(false);
    const auth = useAuth();

    async function init() {
        if (initFlag) return;

        if (step === "loading" || step === "timeout" || step === "successfullyCompleted" || step === "failureCompleted" || step === "reportQuestionForm" || step === "reportQuestionSuccess")
            setStep("home");

        if (step === "userQuizList")
            goToUserQuizList();

        setInitFlag(true);
    }

    async function start() {
        if (auth.user) {
            goToLoading();
            const response = await quizGameService.start();
            setQuizId(response.quizId);
            setQuizQuestionNumber(1);
            setJokers(response.jokers);
            setStep("start");
        } else {
            setStep("guest");
        }
    }

    useEffect(function () {
        if (timerStatus) {
            const interval = setInterval(async function () {
                if (remainingTime > 0) {
                    setRemainingTime(remainingTime - 1);
                } else {
                    stopTimeCountDown();
                    reset();
                    setStep("timeout");
                    await quizGameService.setTimeout(quizId);
                }
            },
                1000);
            return function () {
                clearInterval(interval);
            }
        }
        return function () {

        }
    }, [timerStatus, remainingTime]);

    function startTimeCountDown() {
        setTimerStatus(true);
    }

    function stopTimeCountDown() {
        setTimerStatus(false);
    }

    function goToWait() {
        setStep("wait");
    }

    function goToLoading() {
        setStep("loading");
    }

    async function goToUserQuizList(listType) {
        if (userQuizListType !== listType)
            setUserQuizListType(listType);
        goToLoading();
        if (!userQuizList || userQuizList.length > 0)
            setUserQuizList([]);
        const response = await quizGameService.getUserQuizList(listType);
        setUserQuizList(response.quizzes);
        setStep("userQuizList");
    }

    function reset() {
        setQuizQuestion(null);
        setQuizQuestionId(0);
        setQuizQuestionNumber(0);
        setRemainingTime(0);
        setJokers(null);
    }

    function goToHome() {
        setStep("home");
        reset();
    }

    function showQuestion(questionResponse) {
        setQuizQuestionId(questionResponse.quizQuestionId);
        setQuizQuestion({
            categoryName: questionResponse.categoryName,
            questionText: questionResponse.questionText,
            answers: questionResponse.answers,
            answerTime: questionResponse.answerTime
        });
        setRemainingTime(questionResponse.answerTime);
        setStep("showQuestion");
        startTimeCountDown();
    }

    async function getQuestion() {
        goToLoading();
        const response = await quizGameService.getQuestion(quizId, quizQuestionId);
        showQuestion(response);
    }

    async function sendAnswer(answerOption) {
        if (!answerOption || answerOption.length === 0) return;
        stopTimeCountDown();
        goToLoading();
        setPrevQuizQuestionId(quizQuestionId);
        const response = await quizGameService.sendAnswer(quizId, quizQuestionId, answerOption);
        if (response.isCorrect) {
            setQuizQuestion(null);
            if (response.isCompleted) {
                reset();
                setStep("successfullyCompleted");
            } else {
                setQuizQuestionId(response.nextQuizQuestionId);
                setQuizQuestionNumber(response.nextQuizQuestionNumber);
                setRemainingTime(0);
                setStep("correctAnswer");
                setTimeout(function () {
                    goToWait();
                }, 2000);
            }
        } else {
            if (response.isCompleted) {
                reset();
                setStep("failureCompleted");
            } else {
                quizQuestion.answers.forEach(x => {
                    if (answerOption === x.answerOption) {
                        x.isWrongAnswer = true;
                    }
                });
                setStep("showQuestion");
                startTimeCountDown();
            }
        }
    }

    async function useJoker(quizJoker) {
        if (step !== "showQuestion") return;
        stopTimeCountDown();
        goToLoading();
        const response = await quizGameService.useJoker(quizId, quizQuestionId, quizJoker);
        const newJokers = [...jokers];
        const jokerInfo = newJokers.find(x => x.joker === quizJoker);
        jokerInfo.isUsed = true;
        setJokers(newJokers);
        switch (quizJoker) {
            case QuizJoker.ChangeQuestion:
            case QuizJoker.HalfAndHalf:
                showQuestion(response.question);
                break;
            case QuizJoker.DoubleAnswer:
                setStep("showQuestion");
                startTimeCountDown();
                break;
            case QuizJoker.AdditionalTime:
                setRemainingTime(remainingTime + quizQuestion.answerTime);
                setStep("showQuestion");
                startTimeCountDown();
                break;
        }
    }

    function goToReportQuestionForm() {
        setStep("reportQuestionForm");
    }

    async function reportQuestion(description) {
        goToLoading();
        const response = await quizGameService.reportQuestion(quizId, prevQuizQuestionId, description);

        setStep("reportQuestionSuccess");
    }

    return {
        init,
        goToWait,
        goToLoading,
        goToHome,
        goToUserQuizList,
        step,
        start,
        getQuestion,
        quizQuestionNumber,
        quizQuestion,
        remainingTime,
        userQuizList,
        sendAnswer,
        jokers,
        useJoker,
        goToReportQuestionForm,
        reportQuestion
    }
}


