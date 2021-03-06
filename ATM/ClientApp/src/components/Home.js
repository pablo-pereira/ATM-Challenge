import React, { Component } from 'react';
import axios from "axios";
import './Home.css'
import { Redirect } from "react-router-dom";


export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { cardNumber: "", errorMessage:''};
    // this.updateInput = this.updateInput.bind(this);
  };

 


  cleanCardNumber = ()=>{
    return this.state.cardNumber.split('-').join('');
  }

  updateInput = e => {
    console.log(e.target.value );
    if (e.target.value === 10) {
      this.setState({
        cardNumber: '',
        errorMessage : ''        
      });
      return;
    }
    let char;
    if (this.state.cardNumber.length < 19) {
      if (this.state.cardNumber.length > 1 
          && ((this.cleanCardNumber().length + 1) % 4) === 0
          && this.cleanCardNumber().length < 15) {
        char = e.target.value +"-"
        
      }
      else 
      {
        char = e.target.value
      }

      this.setState({
        cardNumber: this.state.cardNumber + char        
      });      
    }    
  }

  handleSubmit = event => {
    event.preventDefault();
    if ( this.state.cardNumber.length  >=19) {
      axios.get('https://localhost:5001/Card/'+ this.cleanCardNumber())
      .then(res => {
        console.log(res);
        console.log(res.data);

        if(res.status ===204){
          this.setState({            
            errorMessage : 'Wrowng card number.'        
          });
        }
        if(res.status ===409){
          this.setState({            
            errorMessage : res.data        
          });
        }
        if(res.status ===200){
          this.props.history.push("/pin");
          //redirect
        }
      })      
    }
    else{
      this.setState({errorMessage : 'Wrowng card number.' });
    }
  }

  render () {
    const buttons = [];
    for (let index = 1; index < 10; index++) {
      buttons.push(<li  index={index} rel={index} value={index} onClick={this.updateInput.bind(this)}>{index}</li>)
    }
    
    return (
      <div id="container">
        <input className="input-number" readOnly={true} type="text" placeholder="Card Number" value={this.state.cardNumber}/>
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
