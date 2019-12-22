import React from 'react';
import { useGame } from "../../hooks/use-game";
import SVG from 'react-inlinesvg';
import powerOnOff from "../../../svg/iconmonstr-power-on-off-12.svg";

export default function () {
    const game = useGame();
    return (<div className="game-step game-start-step">
        <SVG src={powerOnOff} />
        <p>Yarışma başladı. İlk soruyla başlayın.</p>
        <button className="btn btn-secondary" type="button" onClick={() => game.getQuestion()}>Soruyu gör</button>
    </div>);
}