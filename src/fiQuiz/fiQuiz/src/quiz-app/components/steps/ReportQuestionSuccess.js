import React from 'react';
import { useGame } from "../../hooks/use-game";
import SVG from 'react-inlinesvg';
import warning from "../../../svg/iconmonstr-warning-12.svg";

export default function () {
    const game = useGame();
    return (<div className="game-step game-report-question-success-step">
        <SVG src={warning} />
        <p>Soru hakkında bildirimin alındı. En kısa sürede kontrol edilecek.</p>
        <button className="btn btn-secondary" type="button" onClick={() => game.start()}>Yarışmaya Yeniden Başla</button>
    </div>);
}