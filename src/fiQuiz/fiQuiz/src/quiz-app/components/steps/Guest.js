import React from 'react';
import SVG from 'react-inlinesvg';
import key from "../../../svg/iconmonstr-key-2.svg";

export default function () {
    return (<div className="game-step game-guest-step">
        <SVG src={key} />
        <p>Yarışmaya katılabilmek için giriş yapman gerekiyor.</p>
        <button className="btn btn-secondary" type="button">Giriş yap</button>
    </div>);
}
