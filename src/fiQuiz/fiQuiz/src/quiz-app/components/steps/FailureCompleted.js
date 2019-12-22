import React from 'react';
import { useGame } from "../../hooks/use-game";
import SVG from 'react-inlinesvg';
import error from "../../../svg/iconmonstr-error-8.svg";

export default function () {
    const game = useGame();
    return (<div className="game-step game-failure-completed-step">
        <SVG src={error} />
        <p>Soruya yanlış cevap verdin. Yarışma bitti.</p>
        <button className="btn btn-secondary" type="button" onClick={() => game.start()}>Yarışmaya Yeniden Başla</button>
        <br />
        <button className="btn btn-warning mt-2" type="button" onClick={() => game.goToReportQuestionForm()}>Soruyu bildir</button>
    </div>);
}