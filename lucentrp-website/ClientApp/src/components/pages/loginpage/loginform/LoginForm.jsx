import React from 'react';

import { Formik, Form, Field } from 'formik';
import * as Yup from "yup";

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
const submit = (values, actions) => {
    // TODO: Implement the submit action.
}

/**
 * Create a LoginForm.
 * 
 * @returns The login form.
 */
const LoginForm = () => {
    return (
        <Formik
            initialValues={initalValues}
            validationSchema={validationSchema}
            validateOnChange={false}
            validateOnBlur={false}
            onSubmit={(values, actions) => submit(values, actions)}
        >
            {({ isSubmitting, touched, errors }) => (
                <Form id="login-form">
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