export default (error, handleError) => {
  console.log(error.message)
    const parsedErrors = JSON.parse(error.request.response);
      if(error.response.status === 500){
        parsedErrors.errors = parsedErrors.errors[0].split['.']
      }
      else{  
        handleError((prev) => [...prev, ...parsedErrors.errors]);
      }
}