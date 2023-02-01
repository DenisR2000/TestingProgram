import React, { useEffect, useState } from "react"
import { GET_TESTTS } from '../constants/constant'
import '../styles/TetstList.css'

function TetstList () {

    const [tests, setTests] = useState([])
    const [userId] = useState(sessionStorage.getItem('userId'))

    useEffect(() => {
        var access_token = sessionStorage.getItem('access_token')
        console.log(access_token);
        if(access_token) {
            loadTests()
        } else {
            window.location = '/'
        }
    }, [])

    async function loadTests() {  
        try {
            var result = await fetch(`${GET_TESTTS}/${userId}`)
            if(result.ok === true) {
                const data = await result.json();
                console.log("Result: ", data)
                setTests(data)
            }
        }catch(e) {
            console.log(`Exception: ${e}`)
        }
    }

    function onClickTakeTest(e, item) {
        e.preventDefault()
        window.location = `/passtest/${item.TestId}`
    }

    return(
        <>
            <h1 className="ttitle">Available tests</h1>

            <div className="main__container">
                {tests.map((item, i) => {
                    return(
                        <>
                            <div className="cart">
                                <h2 className="cart__text">{item.TestTitle}</h2>
                                <span style={{fontSize: "19px", color: "lightgray"}} className="cart__text">{item.TestDescription}</span>
                                <button className="wave-btn" onClick={e => onClickTakeTest(e, item)}>
                                    <span className="wave-btn__text">Start</span>
                                    <span className="wave-btn__waves"></span>
                                </button>
                            </div>
                        </>
                    )
                })}
            </div>
        </>
    )
}

export default TetstList