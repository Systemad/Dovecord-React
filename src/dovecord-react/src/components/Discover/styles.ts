import styled from "styled-components";

export const Grid = styled.div`
  display: grid;

  grid-template-columns: 71px 240px auto 240px;
  grid-template-rows: 46px auto 52px;
  grid-template-areas:
        "SS SS SS SS"
        "SS SS SS SS"
        "SL UI CD UL";

  height: 100%;
`;

export const Container = styled.div`
    grid-area: CI;

    display: flex;
    align-items: center;

    padding: 0 17px;
    background-color: var(--primary);
    box-shadow: rgba(0, 0, 0, 0.2) 0px 1px 0px 0px;
    z-index: 2;
`;