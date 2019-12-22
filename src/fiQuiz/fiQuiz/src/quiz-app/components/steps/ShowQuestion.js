import React, { useState } from 'react';
import { useGame } from "../../hooks/use-game";
import SVG from 'react-inlinesvg';
import help from "../../../svg/iconmonstr-help-6.svg";

export default function () {
    const game = useGame();
    const [selectedAnswerOption, setSelectedAnswerOption] = useState();
    if (!game.quizQuestion) return null;
    return (
        <div className="game-step game-show-question-step">
            <p className="question-text">
                <strong>{game.quizQuestionNumber}. Soru - {game.quizQuestion.categoryName}</strong>
                <br />
                {game.quizQuestion.questionText}
            </p>
            <div className="question-answer-list">
                {game.quizQuestion.answers.map(answer =>
                    <div className="custom-control custom-radio custom-control-secondary question-answer" key={answer.answerOption}>
                        <input type="radio" className="custom-control-input" value={answer.answerOption} disabled={answer.isWrongAnswer} name={answer.isWrongAnswer ? "" : "answerOption"} id={"answerOption" + answer.answerOption} checked={selectedAnswerOption === answer.answerOption} onChange={() => setSelectedAnswerOption(answer.answerOption)} />
                        <label className="custom-control-label" htmlFor={"answerOption" + answer.answerOption}>
                            {answer.answer}
                        </label>
                    </div>)}
            </div>
            <button className="btn btn-secondary" type="button" onClick={() => game.sendAnswer(selectedAnswerOption)}>Cevap ver</button>
        </div>);
}