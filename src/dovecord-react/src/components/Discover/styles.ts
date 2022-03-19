import styled from "styled-components";

/*
// SL - Server List
// SN - Server Name
// CI - Channel Info
// CL = Channel List
// CD = Channel Data
// UL = User List
// UI = User Info
 */
/*
    grid-template-areas:
        "SL SN CI CI"
        "SL CL CD UL"
        "SL UI CD UL";
 */
export const Container = styled.div`
    grid-area: CL / UL / UI / UL;

    display: flex;
    align-items: center;

    padding: 0 17px;
    background-color: var(--primary);
    box-shadow: rgba(0, 0, 0, 0.2) 0px 1px 0px 0px;
    z-index: 2;
`;