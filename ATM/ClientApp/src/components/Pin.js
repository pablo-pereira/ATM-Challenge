import React, { Component } from 'react';
import axios from "axios";
import './Home.css'
// import { Redirect } from "react-router-dom";


export class Pin extends Component {
  static displayName = Pin.name;

  constructor(props) {
    super(props);
    this.state = { pinNumber: "", errorMessage:''};
    // this.updateInput = this.updateInput.bind(this);
  };

  updateInput = e => {
    console.log(e.target.value );
    if (e.target.value === 10) {
      this.setState({
        pinNumber: '',
        errorMessage : ''        
      });
      return;
    }    
    if (this.state.pinNumber.length < 4) {
      this.setState({
        pinNumber: this.state.pinNumber + e.target.value        
      });      
    }    
  }

  handleSubmit = event => {
    event.preventDefault();
    if ( this.state.pinNumber.length  >=4) {
      axios.put('https://localhost:5001/Card/'+this.pinNumber)
      .then(res => {
        console.log(res);
        console.log(res.data);

        if(res.status ===204){
          this.setState({            
            errorMessage : 'Wrowng PIN number.'        
          });
        }
        if(res.status ===409){
          this.setState({            
            errorMessage : res.data        
          });
        }
        if(res.status ===200){
          //redirect
          this.props.history.push("/menu");
        }
      })      
    }
    else{
      this.setState({errorMessage : 'Wrowng PIN number.' });
    }
  }

  
  


  render () {
    const buttons = [];
    for (let index = 1; index < 10; index++) {
      buttons.push(<li key={index} rel={index} value={index} onClick={this.updateInput.bind(this)}>{index}</li>)
    }
    
    return (
      <div id="container">
        <input className="input-number" readOnly={true} type="text" placeholder="PIN" value={this.state.pinNumber}/>
        <ul className="numpad">
          {buttons}
          <li rel="delete" value="10" onClick={this.updateInput.bind(this)}>DEL</li>
          <li rel="enter" onClick={this.handleSubmit.bind(this)}>ENTER</li>
        </ul>
        {this.state.errorMessage.length > 0 &&
          <div className="alert alert-danger">
            {this.state.errorMessage}
          </div>
        }
      </div>
    );
  }
}