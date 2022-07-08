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
            .then(data => {
                setData({ friends: data, loading: false })
            })
            .catch(err => console.log(err))
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
        });

        let newFriendsList = [].concat(data.friends);
        console.log(newFriendsList);
        newFriendsList.push(friend);

        setData((prevData) => ({
            ...prevData, friends: newFriendsList
        }));
    }
    const currentDate = new Date().toISOString().substring(5, 10);
    const renderData = () => {
        return (
            <>
                <AddFriendForm addFriend={addNewFriend} />
                <section>
                    {
                        data.friends
                            .filter(f => {
                                const friendDate = f.birthDate.substring(5, 10);
                                return friendDate > currentDate;
                            })
                            .sort((a, b) => (a.birthDate).localeCompare(b.birthDate))
                            .map(f => (<Friend data={f} key={f.birthDate} />))
                    }
                    {
                        data.friends
                            .filter(f => {
                                const friendDate = f.birthDate.substring(5, 10);

                                return friendDate < currentDate;
                            })
                            .sort((a, b) => (a.birthDate).localeCompare(b.birthDate))
                            .map(f => (<Friend data={f} gradient="#BAE0FF" key={f.birthDate} />))
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

export default App;