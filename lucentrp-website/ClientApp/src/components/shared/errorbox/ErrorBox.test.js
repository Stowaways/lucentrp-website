import { render } from '@testing-library/react';

import ErrorBox from './ErrorBox';

describe("ErrorBox test cases", () => {
    test("Error box renders without crashing", () => {
        render(<ErrorBox />);
    });

    test("Error box renders inner content", () => {
        const { container } = render(
            <ErrorBox>
                <p>This is an error message!</p>
            </ErrorBox>
        );

        expect(container.firstChild.textContent).toContain("This is an error message!");
    });
});