import React, { useState, useEffect } from 'react';
import Friend from './components/Friend';
import AddFriendForm from './components/AddFriendForm';
import './App.css';

function App() {
    const [data, setData] = useState({ friends: [], loading: true });

    const populateFriendsData = () => {
        fetch("/friends", { method: "GET" })
            .then(response => {
                if (!response.ok) {
                    throw new Error(
                        `This is an HTTP error: The status is ${response.status}`
                    );
                }
                return response.json();
            })
            .then(respFromApi => {
                setData({ friends: respFromApi, loading: false })
            })
            .catch(err => console.log(err))
    }

    const removeFriend = id => {
        if (!id) return;
        fetch(`friends/${id}`, { method: 'DELETE' })
            .then(() => {
                let newFriendsList = data.friends.filter(f => f.friendId !== id);
                setData({ loading: false, friends: newFriendsList });
            });
    }

    const addNewFriend = friend => {
        if (data.loading) {
            console.log('data is still loading');
            return;
        }

        fetch('/friends', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(friend)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(
                        `This is an HTTP error: The status is ${response.status}`
                    );
                }
                return response.json();
            })
            .then(respFromApi => {
                let newFriendsList = data.friends.concat(respFromApi);
                console.log('new friends list after adding created entity');
                console.log(newFriendsList);
                setData({ loading: false, friends: newFriendsList });
            })
            .catch(err => console.log(err));
    }

    const currentDayOfyear = daysIntoYear(new Date());
    const gradients = ['#6E70DA', '#6BBDD7', '#BAE0FF', '#F8F37B', '#FE4518', '#FEB025', '#BAE0FF', '#6BD7A1', '#AFD0D6', '#AFD0D6', '#F67E7D', '#FFB997'];

    const renderData = () => {
        return (
            <>
                <h1 className="title">Remembersy</h1>
                <AddFriendForm addFriend={addNewFriend} />
                <section>
                    {
                        data.friends
                            .filter(f => {
                                if (!f.dayOfYear) return false;
                                return f.dayOfYear >= currentDayOfyear;
                            })
                            .sort((a, b) => a.dayOfYear - b.dayOfYear)
                            .map(f => (<Friend data={f} key={f.birthDate}
                                removeFriend={removeFriend}
                                gradient={gradients[Math.floor(Math.random() * gradients.length)]} />))
                    }
                    {
                        data.friends
                            .filter(f => {
                                if (!f.dayOfYear) return false;
                                return f.dayOfYear < currentDayOfyear;
                            })
                            .sort((a, b) => a.dayOfYear - b.dayOfYear)
                            .map(f => (<Friend data={f}
                                removeFriend={removeFriend}
                                gradient={gradients[Math.floor(Math.random() * gradients.length)]}
                                key={f.birthDate} />))
                    }
                </section>
            </>
        );
    }

    useEffect(() => {
        populateFriendsData();
    }, []);

    if (data.loading) {
        return <p><em>Loading... Please refresh once the ASP.NET backend has started.</em></p>;
    }

    return renderData();
}

function daysIntoYear(date) {
    return (Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()) - Date.UTC(date.getFullYear(), 0, 0)) / 24 / 60 / 60 / 1000;
}

export default App;