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

    const removeFriend = id => {
        if(!id) return;
        fetch(`friends/${id}`, { method: 'DELETE' })
            .then(() => {
                let newFriendsList = data.friends.filter((_, i) => i !== id)
                const newData = Object.assign(data, {...data, friends: newFriendsList});
                setData(newData);
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
        });

        let newFriendsList = [].concat(data.friends);
        console.log(newFriendsList);
        newFriendsList.push(friend);

        setData((prevData) => ({
            ...prevData, friends: newFriendsList
        }));
    }
    const currentDate = new Date().toISOString().substring(5);
    const gradients = ['#6E70DA', '#6BBDD7', '#BAE0FF', '#F8F37B', '#FE4518', '#FEB025', '#BAE0FF', '#6BD7A1', '#AFD0D6', '#AFD0D6', '#F67E7D', '#FFB997'];
    const renderData = () => {
        return (
            <>
                <AddFriendForm addFriend={addNewFriend} />
                <section>
                    {
                        data.friends
                            .filter(f => {
                                if(!f.birthDate) return false;
                                const friendDate = new Date(f.birthDate).toISOString();
                                return friendDate > currentDate;
                            })
                            .sort((a, b) => (a.birthDate).localeCompare(b.birthDate))
                            .map(f => (<Friend data={f} key={f.birthDate} 
                            removeFriend={removeFriend}
                            gradient={gradients[Math.floor(Math.random()*gradients.length)]}/>))
                    }
                    {
                        data.friends
                            .filter(f => {
                                if(!f.birthDate) return false;
                                const friendDate = new Date(f.birthDate).toISOString();
                                return friendDate <= currentDate; 
                            })
                            .sort((a, b) => (a.birthDate).localeCompare(b.birthDate))
                            .map(f => (<Friend data={f} 
                            removeFriend={removeFriend}
                            gradient={gradients[Math.floor(Math.random()*gradients.length)]} 
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

export default App;