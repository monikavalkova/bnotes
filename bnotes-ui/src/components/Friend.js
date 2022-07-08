import React from 'react';
import '../App.css';

function Friend({data, gradient}){
    const day = data.birthDate.substring(8, 10);
    const month = data.birthDate.substring(5,7);


    const contentStyle = {
        background: 'linear-gradient(90deg, ' + {gradient} + ' 15%, white 15%)',
    }

    return (
       <article className="friendQuickView" 
       style={contentStyle}>
       
            <h3 className="friendCard friendName">{data.firstName}</h3>
            <h5 className="friendCard friendBd">{day}, {convertMonth(month)}</h5>
       </article> 
    );
}

function convertMonth(num) {
    switch(num)
    {
        case "01": return "January"
        case "02": return "February"
        case "03": return "March"
        case "04": return "April"
        case "05": return "May"
        case "06": return "June"
        case "07": return "July"
        case "08": return "August"
        case "09": return "September"
        case "10": return "October"
        case "11": return "November"
        case "12": return "December"
        default: return "error parsing month";
    }
}

export default Friend;