
// window.addEventListener("load", function(){
//   console.log("hi");
//   //everything is fully loaded, don't use me if you can use DOMContentLoaded
// });

function completeTask(thing) {
    let form = thing.closest('form');
    form.submit();
  }

$(document).ready(function() {
  console.log("how?");

  let checkboxes = document.querySelectorAll("input[type=checkbox]");
  for (let checkbox of checkboxes) {

  console.log(`Hi my name is ${checkbox.name}`);
  let cb = `${checkbox.name}`;
  console.log("Is it checked?" + checkbox.checked);

  $(checkbox).on("click", function(event) {
        console.log(checkbox.name + " " + checkbox.checked);
        console.log("target is" + event.target);
        completeTask(checkbox);
      
     });


     


  // let cb = document.querySelector("input");
  // console.log(cb.checked); // false
  // $("input").on("click", function(event) {
  //   console.log(cb.checked);
  // }



  
}


  // $("#z2").on("click", function (event) {
  //     event.preventDefault();
  //     console.log("the event" + event);
  //     console.log("the event target" + event.target);
  //     completeTask(event.target);
        
  // });

  // let inputs = document.querySelectorAll("input");
  // for (let input of inputs) {
  //   alert( input.value + ': ' + input.checked );
  // }

  
  

  // let cb = document.querySelector("input");

  // const cb = document.querySelector("input");
//   console.log(cb.checked); // false
//   $("input").on("click", function(event) {
//     console.log(cb.checked);
//  });

 

});



// import 'bootstrap';
// import 'ToDoList\wwwroot\css\styles.css';
// import 'ToDoList\Models';

//business logic

// function completeTask(checkbox) {
//   let form = checkbox.closest('form');
//   form.submit();
// }

// $(document).ready(function() {
//   $(".itemCheckbox").on("click", function (event) {
//     event.preventDefault();
//     console.log(event);
//     console.log(event.target);
//     completeTask(event.target);
//     console.log(event.target);
//   });

// function completeTask(checkbox) {
//   let form = checkbox.closest('form');
//   form.submit();
// }






    


