import { render } from '@testing-library/react';

import PageNavigationBar from './PageNavigationBar';

describe("PageNavigationaBar test cases", () => {
    test("Page navigation bar renders without crashing", () => {
        render(<PageNavigationBar />);
    });
});