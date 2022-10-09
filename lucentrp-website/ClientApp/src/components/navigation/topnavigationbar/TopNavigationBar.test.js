import { render } from "@testing-library/react"

import TopNavigationBar from "./TopNavigationBar"

describe("TopNavigationBar test cases", () => {
    test("Renders the top navigation bar without crashing", () => {
        render(<TopNavigationBar />);
    });

    test("Renders three divs inside of the navigation bar", () => {
        const { container } = render(<TopNavigationBar />);
        expect(container.firstChild.firstChild.childNodes.length).toBe(3);
    });

    test("Renders left justified content", () => {
        const { container } = render(
            <TopNavigationBar>
                <pre justified="left">Left Test</pre>
            </TopNavigationBar>
        );
        
        expect(container.firstChild.firstChild.childNodes[0].textContent).toContain("Left Test");
    });

    test("Renders center justified content", () => {
        const { container } = render(
            <TopNavigationBar>
                <pre justified="center">Center Test</pre>
            </TopNavigationBar>
        );

        expect(container.firstChild.firstChild.childNodes[1].textContent).toContain("Center Test");
    });

    test("Renders right justified content", () => {
        const { container } = render(
            <TopNavigationBar>
                <pre justified="right">Right Test</pre>
            </TopNavigationBar>
        );

        expect(container.firstChild.firstChild.childNodes[2].textContent).toContain("Right Test");
    });
});
