import styled from "styled-components";

export const Container = styled.div`
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
`

export const Box = styled.div`
  position: relative;
  width: 80vmin;
  border: solid red 2px;
  box-sizing: border-box;
`
export const Content = styled.div`
  position: absolute;
  width: 100%;
  height: 100%;
`