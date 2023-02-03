import React, { useEffect } from "react"
import '../styles/Login.css'

function Login(){

    useEffect(() => {
        var access_token = sessionStorage.getItem('access_token')
        if(access_token) {
            window.location = '/testslist'
        }
    }, [])

    function Logout(e) {
        e.preventDefault()
        sessionStorage.removeItem('access_token')
        sessionStorage.removeItem('userId')
        sessionStorage.removeItem('roles')
        window.location = '/'
    }

    async function onSubmitLogin(e) {
        e.preventDefault()
        var loader = document.getElementById('loading-anim__container')
        loader.style.display = 'flex'
        var userName = String(e.nativeEvent.srcElement[0].value)
        var password = String(e.nativeEvent.srcElement[1].value)
        var body = {
            userName,
            password
        }
        var _response = await fetch('https://localhost:44364/api/account/login', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body) 
        })
        if(_response.ok === true) {
            const jsonData = await _response.json()
            console.log(jsonData.access_token)
            if(jsonData !== null) {
                sessionStorage.setItem('access_token', jsonData.access_token)
                sessionStorage.setItem('userId', jsonData.userId)
                sessionStorage.setItem('roles', jsonData.roles)
                setTimeout(() => { 
                    loader.style.display = 'none'
                    window.location = '/testslist'
                }, 1500)
            }
        }
     }

    return(
        <>
            <div id="loading-anim__container" className="loading-anim__container">
                <svg class="pl" viewBox="0 0 200 200" width="200" height="200" xmlns="http://www.w3.org/2000/svg">
                    <defs>
                        <linearGradient id="pl-grad1" x1="1" y1="0.5" x2="0" y2="0.5">
                            <stop offset="0%" stop-color="hsl(313,90%,55%)" />
                            <stop offset="100%" stop-color="hsl(223,90%,55%)" />
                        </linearGradient>
                        <linearGradient id="pl-grad2" x1="0" y1="0" x2="0" y2="1">
                            <stop offset="0%" stop-color="hsl(313,90%,55%)" />
                            <stop offset="100%" stop-color="hsl(223,90%,55%)" />
                        </linearGradient>
                    </defs>
                    <circle class="pl__ring" cx="100" cy="100" r="82" fill="none" stroke="url(#pl-grad1)" stroke-width="36" stroke-dasharray="0 257 1 257" stroke-dashoffset="0.01" stroke-linecap="round" transform="rotate(-90,100,100)" />
                    <line class="pl__ball" stroke="url(#pl-grad2)" x1="100" y1="18" x2="100.01" y2="182" stroke-width="36" stroke-dasharray="1 165" stroke-linecap="round" />
                </svg>
            </div>
            <main className="login-box">
                <h2>Login</h2>
                <form onSubmit={e => onSubmitLogin(e)}>
                    <div className="user-box">
                        <input defaultValue="User1Dew0777" id="userName" type="text" name="username"/>
                        <label>UserName</label>
                    </div>
                    <div className="user-box">
                        <input defaultValue="user050103SANDadmin$" id="password" type="password" name="password"/>
                        <label>Password</label>
                    </div>
                    <div className="user-box">
                        <a href="#">
                            <button type="submit">
                                Submit
                                <span>
                                    <span></span>
                                </span>
                                <span></span>
                                <span></span>
                                <span></span>
                            </button>
                        </a>
                    </div>
                </form>
            </main>
        </>
    )  
}

export default Login