import { render } from '@testing-library/react';

import TopNavigationButton from './TopNavigationButton';

describe("Top Navigation Button test cases", () => {
    test("Renders a button without crashing", () => {
        render(<TopNavigationButton href="/"/>)
    });

    test("Renders the inner content of the button", () => {
        const { container } = render(<TopNavigationButton href="/" content={<p>Test content</p>} />);
        expect(container.firstChild.textContent).toContain("Test content");
    });
});
