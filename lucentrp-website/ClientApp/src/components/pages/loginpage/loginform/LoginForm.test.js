import { render } from '@testing-library/react';

import LoginForm from './LoginForm';

describe("Login Form test cases", () => {
    test("Login form renders without crashing", () => {
        render(<LoginForm />);
    });
});
