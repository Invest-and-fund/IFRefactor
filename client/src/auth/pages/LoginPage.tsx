// import React from 'react'
import reactLogo from "../assets/react.svg";
import {useState} from "react";

export default function LoginPage () {
    const [count, setCount] = useState(0)
    return (
        <div className="container">
            <marquee className="text-marqee" direction="left">
                <h2 className="big-title">THE LOGIN FORM HERE</h2>
            </marquee>
        </div>
    )
}
