import React, {useState} from 'react';

import { Formik, Form, Field } from 'formik';
import * as Yup from "yup";

import ErrorBox from '../../../shared/errorbox/ErrorBox';
import ErrorIcon from '../../../shared/icons/ErrorIcon';

import * as UserAccountServices from '../../../../services/userAccountServices';

/**
 * The login form's initial values.
 */
const initalValues = {
    username: "",
    password: ""
}

/**
 * The login form's validation schema.
 */
const validationSchema = Yup.object().shape({
    username: Yup.string().required(`Username is required`),
    password: Yup.string().required(`Password is required`)
});

/**
 * The function that will handle submitting the form.
 * 
 * @param {*} values The form values. 
 * @param {*} actions Actions that can be performed to the form.
 */
const submit = async (values, formikHelpers, setServerError) => {
    const loginResponse = await UserAccountServices.login(values);

    // If the wrong credentials were specified.
    if (loginResponse.status === 401) {
        setServerError({
            occurred: true,
            message: "You have entered an incorrect username or password!"
        });

        formikHelpers.resetForm();
        formikHelpers.setSubmitting(false);
        return;
    }

    // If an error occurred .
    if (loginResponse.status !== 200) {
        setServerError({
            occurred: true,
            message: "An internal server error occurred, please try again later!"
        });

        formikHelpers.resetForm();
        formikHelpers.setSubmitting(false);
        return;
    }

    // If the user logged in successfully, redirect them to the dashboard.
    window.location.href = "dashboard";
}

/**
 * Create a LoginForm.
 * 
 * @returns The login form.
 */
const LoginForm = () => {
    const [serverError, setServerError] = useState({
        occurred: false,
        message: ""
    })

    return (
        <Formik
            initialValues={initalValues}
            validationSchema={validationSchema}
            validateOnChange={false}
            validateOnBlur={false}
            onSubmit={(values, actions) => submit(values, actions, setServerError)}
        >
            {({ isSubmitting, touched, errors }) => (
                <Form id="login-form">
                    {serverError.occurred &&
                        <ErrorBox>
                            <ErrorIcon className="small-icon" onClick={() => setServerError({ occurred: false, message: "" })} />
                            <p>{serverError.message}</p>
                        </ErrorBox>
                    }
                    <div>
                        <div className="form-errror">{(touched.username && errors.username) ? errors.username : <>&nbsp;</>}</div>
                        <label htmlFor="username">Username</label>
                        <Field id="username" className="form-text-input" type="text" name="username" />
                    </div>
                    <div>
                        <div className="form-errror">{(touched.password && errors.password) ? errors.password : <>&nbsp;</>}</div>
                        <label htmlFor="password">Password</label>
                        <Field id="password" className="form-text-input" type="password" name="password" />
                    </div>
                    <div className="form-button-container">
                        <button className="form-button" type="submit" disabled={isSubmitting}>Login</button>
                        <button className="form-button" type="reset" disabled={isSubmitting}>Reset</button>
                    </div>
                </Form>
            )}
        </Formik>
    );
}

export default LoginForm;