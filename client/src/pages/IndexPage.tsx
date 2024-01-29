// import React from 'react'
import reactLogo from "../assets/react.svg";
import "./IndexPage.scss"
import {useState} from "react";

export function IndexPage() {
    const [count, setCount] = useState(0)
    return (<>
        <div className={"container mt-5 pt-5"}>
            <div className="row">
                <div className="col-sm-7">
                    <div className="landing-title">
                        <h5 className={"subtitle introducing"}>Introducing</h5>
                        <h2>Unlimited Sales & Purchases</h2>
                        <h4 className="sub-title">The automatic payment service to manage your finances. The charge card will let you accept payments and buy anything instantly.</h4>
                        <ul className={'listo1'}>
                            <li><span>No more delays</span></li>
                            <li><span>No to debts & loans</span></li>
                            <li><span>One to one payments</span></li>
                        </ul>
                    </div>
                </div>
                <div className="col-sm-5">
                    <img src="/charge-card.png" height="700" />
                </div>
            </div>
            <marquee className="text-marqee" direction="left">
                <h2 className="big-title">Faster, Secure &amp; Instant Payments</h2>
            </marquee>
            <div className={"row text-center my-5 py-5"}>
                <div className="col-sm-4">
                    <img className="mw-100" width="500" src="https://media.istockphoto.com/id/1224570327/vector/paper-plane-creative-symbol-continuous-one-line-drawing-minimalist-style-vector-illustration.jpg?s=2048x2048&w=is&k=20&c=cwVEItcazbs0bM2ODvdNOtiZ1N0w-YWE5cu_OuEeu68=" />
                </div>
                <div className="col-sm-6">
                    <div className="landing-title">
                        <h5 className="sub-title">Automatic. Fast. And Instant.</h5>
                        <h2>Money Anywhere</h2>
                        <p>Access all your money anywhere even the money owed and use it to purchase goods and services without any wait.</p>
                    </div>
                </div>
            </div>
        </div>
        <div className={"container mb-5 pb-5"}>
            <div className="row my-5 py-5">
                <div className="col-lg-6">
                    <div className="landing-title">
                        <h5 className="sub-title">Fully commercialised</h5>
                        <h2>Easy access to travel</h2>
                        <p className="m-0">The charge card allows you to travel anywhere unlimitedly</p>
                    </div>
                    <div>

                    </div>
                </div>
                <div className="col-lg-6">
                    <img className="mw-100" src="https://media.istockphoto.com/id/955952680/photo/passengers-commercial-airplane-flying-above-clouds.jpg?s=2048x2048&w=is&k=20&c=PXyelmoevDbxy6Z1jWSnhaoPl3LWyTK5NVGxNPOt3dA=" />
                </div>
            </div>


            <div className="row my-5 py-5 security">
                <div className="col-sm-12 my-5">
                        <div className="trans">
                            <h2>Privacy & Security First</h2>
                        </div>
                        <div className="landing-title">
                            <h5 className="sub-title">Anonymous sales & purchases</h5>
                            <h2>Biometric Information</h2>
                            <div className="row">
                                <div className="col-sm-6">
                                    <p>Your charge card and Acharge handheld device is unique this makes your purchases and sales unique without revealing your personal data.</p>
                                    <p>All your biometric information is stored securely on your charge card and Acharge handheld device and never stored in any of our database.</p>
                                </div>
                                <div className="col-sm-6">
                                    <img className="mw-100" src="https://media.istockphoto.com/id/1474881248/photo/blank-turned-on-flexible-chamshell-phone-display-half-folded-mockup.jpg?s=2048x2048&w=is&k=20&c=2P0p5kLb6su9FgapZey5vD6mKX6Mjsv-qo45DYygnvg=" />
                                </div>
                            </div>
                        </div>
                </div>
            </div>



        </div>
        </>
    )
}
