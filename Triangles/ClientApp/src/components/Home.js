import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = { coordinates: [], loading: true };
        this.renderTriangles = this.renderTriangles.bind(this);
        this.getPixelsToRender = this.getPixelsToRender.bind(this);
        this.clear = this.clear.bind(this);
        this.findTriangle = this.findTriangle.bind(this);
        this.getTriangle = this.getTriangle.bind(this);
    }

    clear() {
        const canvas = this.refs.canvas;
        const ctx = canvas.getContext('2d');
        ctx.clearRect(0,0,60,60);
    }

    findTriangle() {
        const vertex1 = this.refs.vertex1.value;
        const vertex2 = this.refs.vertex2.value;;
        const vertex3 = this.refs.vertex3.value;;

        const vertexes = [vertex1, vertex2, vertex3];
        let validVertexes = true;
        for(let vertex of vertexes){
            let vertexParts = vertex.split(",");
            if (vertexParts.length !== 2) {
                validVertexes = false; 
            } else {
                let part1 = parseInt(vertexParts[0]);
                let part2 = parseInt(vertexParts[1]);

                if (isNaN(part1) || isNaN(part2)) {
                    validVertexes = false; 
                } 
            }
            if (!validVertexes) {
                break;
            }
        }
                
        if (validVertexes) {
            this.getTriangle(vertex1, vertex2, vertex3);
        } else {
            alert("please enter a vertex in the format XX,YY e.g. 0,10, \nYour coordinates need to be multiples of 10 between 0 and 60");
        }
    }

    renderTriangles() {
       
        const input = this.refs.input;
        const value = input.value;
        if (value === "INVALID COORDINATES") {
            alert("Invalid coordinates");
            return;
        }
        let invalidInput = false;
        let numberPart;
        let firstCharacter;
        if (value.length > 3 && value.length < 1 ) {
            invalidInput = true;
        } else {
            const VALID_ROWS = ["A", "B", "C", "D", "E", "F"];
            firstCharacter = value.substring(0, 1).toUpperCase();
            numberPart = value.substring(1, value.length);
            if (numberPart.length === 2 && numberPart.substring(0, 1) === "0") {
                invalidInput = true;
            }else if (VALID_ROWS.indexOf(firstCharacter) === -1) {
                invalidInput = true;
            } else {
                numberPart = parseInt(numberPart);
                if (isNaN(numberPart)) {
                    invalidInput = true;
                } else {
                    if (numberPart > 12 || numberPart < 1) {
                        invalidInput = true;
                    }
                }
            }
        }
        if (invalidInput) {
            alert("Please enter a valid input");
        } else {
            this.getPixelsToRender(firstCharacter, numberPart);
        }
    }

    async getTriangle(vertex1, vertex2, vertex3) {
        const response = await fetch('locator/' + vertex1 + '/' + vertex2 + '/' + vertex3);
        let data = await response.json();
            

        const input = this.refs.input;
        input.value = data[0];
        this.renderTriangles();
    }

    async getPixelsToRender(firstCharacter, numberPart) {
        const response = await fetch('renderer/' + firstCharacter + '/' + numberPart);
        const data = await response.json();

        const canvas = this.refs.canvas;
        const ctx = canvas.getContext('2d');

        for (let coordinate of data) {
            ctx.fillRect(coordinate.x, coordinate.y, 1, 1);
        }
    }

    render() {
 
        return (
            <div>
                <div>
                    <span>
                        <h5>Please enter an area A-F and 1-12</h5>
                        <input ref="input" type="text"/>
                    </span>
                
                    <button onClick={this.renderTriangles}>Render</button>
                    <button onClick={this.clear}>Clear</button>
                    <canvas ref="canvas" width="60" height="60" className="canvascss">
                        An alternative text describing what your canvas displays.
                    </canvas>
                </div>

                <div>
                    <br />
                    <br />
                    <br />
                    <br />

                    <span>
                        <h5>Please enter a vertex x,y</h5>
                        <input ref="vertex1" type="text" />
                        <h5>Please enter a vertex x,y</h5>
                        <input ref="vertex2" type="text" />
                        <h5>Please enter a vertex x,y</h5>
                        <input ref="vertex3" type="text" />
                    </span>

                    <button onClick={this.findTriangle}>Find Triangle</button>
                </div>
            </div>
        );
    }


}




