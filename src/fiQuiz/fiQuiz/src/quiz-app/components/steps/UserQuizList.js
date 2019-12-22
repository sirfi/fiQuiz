import React from 'react';
import moment from 'moment';
import { useGame } from "../../hooks/use-game";
import { useAuth } from "../../hooks/use-auth";

moment.locale("tr");

export default function () {
    const auth = useAuth();
    const game = useGame();
    return (<div className="game-step game-user-quiz-list-step">
        <table className="table table-sm">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Başlama zamanı</th>
                    <th>Bitirme zamanı</th>
                    <th>Bitme şekli</th>
                    <th>Doğru sayısı</th>
                </tr>
            </thead>
            <tbody>
                {game.userQuizList.map((q, qi) =>
                    <tr key={qi + 1}>
                        <th>{qi + 1}</th>
                        <td>{moment(q.startedAt).fromNow()}</td>
                        <td>{moment(q.completedAt).fromNow()}</td>
                        <td>{q.completionTypeText}</td>
                        <td>{q.correctAnswerCount}</td>
                    </tr>)}
            </tbody>
        </table>
    </div>);
}