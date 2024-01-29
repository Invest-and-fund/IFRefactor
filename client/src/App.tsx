import {Outlet} from 'react-router-dom'
import './App.scss'
import {Link} from "react-router-dom";

function App() {

  return (
      <>
          <div className="main-top"></div>
          <div className="sticky-header">
              <header className="sticky">
                    <nav className="navbar navbar-dark navbar-expand-xl fixed-top">
                      <a className="navbar-brand p-0" href="#">
                          <img className="img-fluid" src="~/images/logo_invert.svg" height="35" alt="" />
                      </a>
                      <button className="navbar-toggler navabr_btn-set custom_nav" type="button" data-bs-toggle="collapse" data-bs-target="#navbarDefault" aria-controls="navbarDefault" aria-expanded="false" aria-label="Toggle navigation"><span></span><span></span><span></span></button>
                      <div className="navbar-collapse justify-content-center collapse hidenav" id="navbarDefault">
                          <ul className="navbar-nav navbar_nav_modify" id="scroll-spy">
                              <li className="nav-item">
                                  <Link className="nav-link" to="/">Home</Link>
                              </li>
                              <li className="nav-item">
                                  <Link className="nav-link" to="/auth/register">Register</Link>
                              </li>
                              <li className="nav-item">
                                  <Link className="nav-link" to="/auth/login">Login</Link>
                              </li>
                          </ul>
                      </div>
                      <div className="create-btn rounded-pill mt-2 me-3">
                          <Link className="nav-link" to="/charges/create">Create Charge</Link>
                      </div>
                  </nav>
              </header>
          </div>
          <Outlet />
    </>
  )
}

export default App
