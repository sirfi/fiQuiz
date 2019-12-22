import React from 'react';
import { useGame } from "../../hooks/use-game";
import SVG from 'react-inlinesvg';
import arrow from "../../../svg/iconmonstr-arrow-32.svg";

export default function () {
    const game = useGame();
    return (<div className="game-step game-wait-step">
        <SVG src={arrow} />
        <p>Sıradaki soruyu yani {game.quizQuestionNumber}. soruyla devam edin.</p>
        <button className="btn btn-secondary" type="button" onClick={() => game.getQuestion()}>Soruyu gör</button>
    </div>);
}