// Credit: https://usehooks.com/useWindowSize/
import { useState, useEffect } from 'react';

/**
 * A hook to get the window size.
 * 
 * @returns The useWindowSize hook.
 */
const useWindowSize = () => {
    const [windowSize, setWindowSize] = useState({
        width: undefined,
        height: undefined,
    });

    useEffect(() => {
        function handleResize() {
            setWindowSize({
                width: window.innerWidth,
                height: window.innerHeight,
            });
        }
        window.addEventListener("resize", handleResize);
        handleResize();
        return () => window.removeEventListener("resize", handleResize);
    }, []);
    
    return windowSize;
}

export default useWindowSize;
