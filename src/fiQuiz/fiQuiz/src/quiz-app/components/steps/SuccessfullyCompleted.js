import React from 'react';
import { useGame } from "../../hooks/use-game";
import SVG from 'react-inlinesvg';
import award from "../../../svg/iconmonstr-award-17.svg";

export default function () {
    const game = useGame();
    return (<div className="game-step game-successfully-step">
        <SVG src={award} />S
        <p>Yarışmayı tamamladın. Tebrikler.</p>
        <button className="btn btn-secondary" type="button" onClick={() => game.start()}>Yarışmaya Yeniden Başla</button>
    </div>);
}