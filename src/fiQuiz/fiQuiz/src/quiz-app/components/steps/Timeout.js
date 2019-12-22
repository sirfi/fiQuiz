import React from 'react';
import { useGame } from "../../hooks/use-game";
import SVG from 'react-inlinesvg';
import time from "../../../svg/iconmonstr-time-21.svg";

export default function () {
    const game = useGame();
    return (<div className="game-step game-timeout-step">
        <SVG src={time} />
        <p>Soruya verilen süre içinde cevap veremediniz.</p>
        <button className="btn btn-secondary" type="button" onClick={() => game.start()}>Yarışmaya Yeniden Başla</button>
    </div>);
}