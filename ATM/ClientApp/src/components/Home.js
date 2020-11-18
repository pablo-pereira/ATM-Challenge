import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div id="container">
          <ul id="keyboard">   
            <li class="letter">1</li>  
            <li class="letter">2</li>  
            <li class="letter">3</li>  
            <li class="letter clearl">4</li>  
            <li class="letter">5</li>  
            <li class="letter">6</li> 
          
            <li class="letter clearl">7</li>  
            <li class="letter ">8</li>  
            <li class="letter">9</li>  
            <li class="letter">0</li>
            <li class="switch">abc</li>  
            <li class="return">retur</li>
            <li class="delete lastitem"></li>  
        </ul>
      </div>
    );
  }
}
