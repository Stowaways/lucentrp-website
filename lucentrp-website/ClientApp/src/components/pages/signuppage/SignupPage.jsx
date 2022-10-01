import React from "react";

import PageNavigationBar from "../PageNavigationBar";
import FullPageSection from "../../layout/fullpagesection/FullPageSection";

import "./signuppage.css"
import Divider from "../../layout/divider/Divider";

const SignupPage = () => {
    return (
        <React.Fragment>
            <PageNavigationBar />
            <FullPageSection id="signup" subtractNav={true}>
                <Divider className="split-form-divider">
                    <div className="center image-box">
                        <h1>Welcome to the party!</h1>
                    </div>
                    <div className="center form-wrapper">
                    </div>
                </Divider>
            </FullPageSection>
        </React.Fragment>
    );
}

export default SignupPage;