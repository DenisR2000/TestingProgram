import React from "react"
import { BrowserRouter as Router, Switch, Route, NavLink } from 'react-router-dom'
import Login from './Login'
import TetstList from './TetstList'
import PassingTest from './PassingTest'

function App() {
    return(
        <>
        <Router>
            <Switch>
                <Route exact path="/" component={Login}></Route>
                <Route exact path="/testslist" component={TetstList}></Route>
                <Route exact path="/passtest/:id" component={PassingTest}></Route>
            </Switch>
        </Router>
        </>
    )
}

export default App