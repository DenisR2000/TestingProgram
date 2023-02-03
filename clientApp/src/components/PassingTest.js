import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { GET_QUESTIONS } from '../constants/constant'
import '../styles/PassingTest.css'
import $ from 'jquery'

const MAX_VISIBILITY = 1;

var answers = new Object()

function Card ({title, listAnsvers}) {

    function onChangeAnsver(e, answer, i) {
        var id = answer.QestionId
        answers[id] = answer.AnswerId

        var radioitems = document.querySelectorAll('.readiobutton__item')
        radioitems.forEach(el => {
            el.classList.remove('active')

        });
        var redios = document.querySelectorAll('input[type=radio]')
        redios.forEach((el) => {
            el.checked = false
        })
        var currentIetemToChecked = document.getElementById(`item${answer.AnswerId}`)
        currentIetemToChecked.classList.toggle('active')

        var currentRudio = document.getElementById(`radio${answer.AnswerId}`)
        currentRudio.checked = true
        // $('.readiobutton__item').find('input').prop('checked', true)
        // $.each($('.readiobutton__item'), function(index, val) {
        //     if($('.readiobutton__item').find('input').prop('checked')==true) {
        //         $('.readiobutton__item').addClass('active')
        //     }
        // })
        // $('.readiobutton__item').parents('.radiobuttons').find('.readiobutton__item').removeClass('active')
        // $('.readiobutton__item').parents('.radiobuttons').find('.  readiobutton__item input').prop('checked', false)
        // $('.readiobutton__item').toggleClass('active')
    }

    return(
        <>
        <div className='card'>
            <h2 className='ttitle'>{title}</h2>
            <div className='block-form__input'>
                <div id='radiobuttons' className='radiobuttons'> 
                    {listAnsvers.map((answer, i) => {
                        return(
                            <>
                                <div id={`item${answer.AnswerId}`} onClick={e => onChangeAnsver(e, answer, i)} className="readiobutton__item">
                                    {answer.AnswerToQuestion}
                                    <input id={`radio${answer.AnswerId}`} checked type="radio" value={answer.AnswerToQuestion}  name={answer.QestionId} />
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
        var loader = document.getElementById('loading-anim__container')
        loader.style.display = 'flex'
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
            setTimeout(() => {
                loader.style.display = 'none'
                let popup = document.getElementById('popup')
                let popup_content = document.getElementById('popup__content')
                popup.style.visibility = 'visible'
                popup.style.opacity = 1
                popup_content.classList.add('animate')
                popup.style.transition = "all 0.5s"
                var mainInfo = document.getElementById('mainInfo')
                mainInfo.innerText = `Number of correct answers ${data}/${count}`
            }, 1200)
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
                <button onClick={e => onSubmit(e)} className="wave-btn" >
                    <span className="wave-btn__text">Finish</span>
                    <span className="wave-btn__waves"></span>
                </button>
            </div>
        : ''}
        {active < count - 1 && 
         <div className='btn__container'>
            <button style={{disabled: 'true'}} className='nav' onClick={() => setActive(i => i + 1)}>Next</button>
         </div>
        }
      </div>
    );
  };

function PassingTest(props) {

    const { id } = useParams();

    const [qestinos, setQestions] = useState([])
    //useLayoutEffect(() => {}, [])

    useEffect(() => {
        var access_token = sessionStorage.getItem('access_token')
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

                setQestions(data)
            }
        }catch(e) {
            console.log(`Exception: ${e}`)
        }
    }

    function onClickGoBack() {
        window.location = '/testslist'
    }

    return(
        <>
            <script>
                alert('hello')
            </script>
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
                    <circle className="pl__ring" cx="100" cy="100" r="82" fill="none" stroke="url(#pl-grad1)" stroke-width="36" stroke-dasharray="0 257 1 257" stroke-dashoffset="0.01" stroke-linecap="round" transform="rotate(-90,100,100)" />
                    <line className="pl__ball" stroke="url(#pl-grad2)" x1="100" y1="18" x2="100.01" y2="182" stroke-width="36" stroke-dasharray="1 165" stroke-linecap="round" />
                </svg>
            </div>
            <div className="popup" id="popup">
                <div className="popup__body">
                    <div style={{position: 'relative'}} id="popup__content" className="popup__content">
                        <a className="popup__close">X</a>
                        <div id="modal-content" className="modal-content">
                            <h2 className="title" style={{color: "#000"}}>Your result</h2>
                            <div className="big-news-container" style={{display: 'flex', justifyContent: "center", flexDirection: 'column', minHeight: "185px"}}>
                                <p>
                                    <br />
                                    <span id='mainInfo' className='mainInfo' style={{ fontSize: '22px', fontFamily: 'sans-serif' }}></span>
                                </p>
                                <button style={{height: "60px", width: 'auto', padding: '10px', boxShadow: '0px 7px 7px rgba(90, 92, 211, 0.4)', color: '#fff', borderRadius: '10px', background: "#4973ff"}} className='bt-back' onClick={onClickGoBack}>Got to tests list</button>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
            <Carousel>
                {qestinos.map((qestin, i, id) => (
                    <Card  title={qestin.QestionName} listAnsvers={qestin.Answers} />
                ))}
            </Carousel>
        </>
    )
}

export default PassingTest