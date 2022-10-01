import React from "react";

import FullPageSection from "../../layout/fullpagesection/FullPageSection";
import PageNavigationBar from "../PageNavigationBar";
import Divider from "../../layout/divider/Divider";

import LoginForm from "./loginform/LoginForm";

import "./loginpage.css";

/**
 * Create a login page.
 * 
 * @returns The login page.
 */
const LoginPage = () => {
    return (
        <React.Fragment>
            <PageNavigationBar />
            <FullPageSection id="login" subtractNav={true}>
                <Divider grid={true}>
                    <div className="image-box">
                        <h1>Welcome back!</h1>
                    </div>
                    <div className="form-wrapper">
                        <LoginForm />
                    </div>
                </Divider>
            </FullPageSection>
        </React.Fragment>
    );
}

export default LoginPage;