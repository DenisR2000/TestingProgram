import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { GET_QUESTIONS } from '../constants/constant'
import '../styles/PassingTest.css'
import $ from 'jquery'

const MAX_VISIBILITY = 1;

var answers = new Object()

function Card ({title, listAnsvers}) {
    function onChangeAnsver(e, answer) {
        var id = answer.QestionId
        answers[id] = answer.AnswerId
    } 

    return(
        <>
        <div className='card'>
            <h2 className='ttitle'>{title}</h2>
            <div className='block-form__input'>
                <div className='radiobuttons'> 
                    {listAnsvers.map((answer, i) => {
                        return(
                            <>
                                <div  className="readiobutton__item">
                                    {answer.AnswerToQuestion}
                                    <input onChange={e => onChangeAnsver(e, answer)} type="radio" value={answer.AnswerToQuestion}  name={answer.QestionId} />
                                </div>
                            </>
                        )
                    }) }
                </div>
            </div>
        </div>
        </>
    )
  }

const Carousel = ({children}) => {

    const [active, setActive] = useState(0);
    const count = React.Children.count(children);
    const { id } = useParams();

    async function onSubmit(e) {
        e.preventDefault()
        var body = {
            answers,
            testId: id
        }
        var _response = await fetch('https://localhost:44364/api/Testing/CheckAnswers', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body) 
        })
        if(_response.ok === true) {
            const data = await _response.json();
            console.log("Ok result: ", data);
        }
    }
    
    return (
      <div className='carousel'>
        {/* {active > 0 && <button className='nav left' onClick={() => setActive(i => i - 1)}>Prev</button>} */}
        {React.Children.map(children, (child, i) => (
          <div className='card-container' style={{
              '--active': i === active ? 1 : 0,
              '--offset': (active - i) / 3,
              '--direction': Math.sign(active - i),
              '--abs-offset': Math.abs(active - i) / 3,
              'pointer-events': active === i ? 'auto' : 'none',
              'opacity': Math.abs(active - i) >= MAX_VISIBILITY ? '0' : '1',
              'display': Math.abs(active - i) > MAX_VISIBILITY ? 'none' : 'block',
            }}>
            {child}
          </div>
        ))}
        {active == count - 1 ? 
            <div className='btn__container'>
                <button onClick={e => onSubmit(e)} style={{posiotin: 'absolute'}} className="wave-btn" >
                    <span className="wave-btn__text">Finish</span>
                    <span className="wave-btn__waves"></span>
                </button>
            </div>
        : ''}
        {active < count - 1 && <button style={{disabled: 'true'}} className='nav right' onClick={() => setActive(i => i + 1)}>Next</button>}
      </div>
    );
  };

function PassingTest(props) {

    const { id } = useParams();

    const [qestinos, setQestions] = useState([])
    //useLayoutEffect(() => {}, [])

    useEffect(() => {
        var access_token = sessionStorage.getItem('access_token')
        console.log(access_token);
        if(access_token) {
            loadQestions()
        } else {
            window.location = '/'
        }
    }, [])

    async function loadQestions() {  
        try {
            var result = await fetch(`${GET_QUESTIONS}/${id}`)
            if(result.ok === true) {
                const data = await result.json();
                console.log("Result: ", data)
                setQestions(data)
            }
        }catch(e) {
            console.log(`Exception: ${e}`)
        }
    }

    return(
        <>
            <Carousel>
                {qestinos.map((qestin, i, id) => (
                    <Card  title={qestin.QestionName} listAnsvers={qestin.Answers} />
                ))}
            </Carousel>
        </>
    )
}

export default PassingTest