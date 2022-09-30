import { render } from "@testing-library/react";

import Divider from "./Divider";

describe("Divider test cases", () => {
    test("Renders a divider without crashing", () => {
        render(<Divider />)
    });

    test("Renders a flex divider by default", () => {
        const { container } = render(<Divider />);
        expect(container.getElementsByClassName("divider-flex").length).toBe(1);
    })

    test("Renders a flex divider by setting grid to false", () => {
        const { container } = render(<Divider grid={false} />);
        expect(container.getElementsByClassName("divider-flex").length).toBe(1);
    })

    test("Renders a grid divider by setting grid to true", () => {
        const { container } = render(<Divider grid={true} />);
        expect(container.getElementsByClassName("divider-grid").length).toBe(1);
    })
})