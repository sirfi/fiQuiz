import axios from "axios";

const serviceApiPath = "/api/QuizGame/";

async function start() {
    const response = await axios.post(serviceApiPath + "Start", {});
    return response.data;
}

async function getQuestion(quizId, quizQuestionId) {
    const response = await axios.post(serviceApiPath + "GetQuestion", {
        quizId: quizId,
        quizQuestionId: quizQuestionId
    });
    return response.data;
}

export const AnswerOption =
{
    A: "A",
    B: "B",
    C: "C",
    D: "D"
}

async function sendAnswer(quizId, quizQuestionId, answerOption) {
    const response = await axios.post(serviceApiPath + "SendAnswer",
        {
            quizId: quizId,
            quizQuestionId: quizQuestionId,
            answerOption: answerOption
        });
    return response.data;
}

export const QuizJoker = {
    ChangeQuestion: "ChangeQuestion",
    HalfAndHalf: "HalfAndHalf",
    DoubleAnswer: "DoubleAnswer",
    AdditionalTime: "AdditionalTime"
}

async function useJoker(quizId, quizQuestionId, joker) {
    const response = await axios.post(serviceApiPath + "UseJoker",
        {
            quizId: quizId,
            quizQuestionId: quizQuestionId,
            joker: joker
        });
    return response.data;
}

async function setTimeout(quizId) {
    const response = await axios.post(serviceApiPath + "SetTimeout",
        {
            quizId: quizId
        });
    return response.data;
}

export const UserQuizListType = {
    Last: "Last",
    Top: "Top"
}

async function getUserQuizList(listType) {
    const response = await axios.post(serviceApiPath + "GetUserQuizList",
        {
            listType: listType
        });
    return response.data;
}

export const QuizCompletionType = {
    Successful: "Successful",
    WrongAnswer: "WrongAnswer",
    Timeout: "Timeout"
}

async function reportQuestion(quizId, quizQuestionId, description) {
    const response = await axios.post(serviceApiPath + "ReportQuestion",
        {
            quizId: quizId,
            quizQuestionId: quizQuestionId,
            description: description
        });
    return response.data;

}

const quizGameService = window.QuizGameService = {
    start,
    getQuestion,
    sendAnswer,
    useJoker,
    setTimeout,
    getUserQuizList,
    reportQuestion
};


export default quizGameService;