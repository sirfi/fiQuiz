import React, { useState } from 'react';
import { useGame } from "../../hooks/use-game";
import SVG from 'react-inlinesvg';
import warning from "../../../svg/iconmonstr-warning-12.svg";

export default function () {
    const game = useGame();
    const [description, setDescription] = useState();
    const submitReport = function () {
        if (description && description.length > 0) {
            game.reportQuestion(description);
            setDescription("");
        }
    };
    return (<div className="game-step game-report-question-form-step">
        <SVG src={warning} />
        <p>Soru bildirimi için açıklama giriniz.</p>
        <div className="form-group">
            <textarea className="form-control" onChange={(event) => setDescription(event.target.value)} placeholder="Açıklama" required>{description}</textarea>
        </div>
        <p>
            <button className="btn btn-secondary" type="button" onClick={submitReport}>Bildirimi gönder</button>
        </p>
    </div>);
}