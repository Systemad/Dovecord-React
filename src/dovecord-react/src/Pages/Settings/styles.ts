import styled from "styled-components";

/*
    grid-template-areas:
        "SL SN CI CI"
        "SL CL CD UL"
        "SL UI CD UL";
 */
export const Container = styled.div`
    grid-area: CD;
    
    display: flex;
    align-items: center;

  padding: 1rem;
    background-color: var(--primary);
    box-shadow: rgba(0, 0, 0, 0.2) 0px 1px 0px 0px;
    z-index: 2;
`;