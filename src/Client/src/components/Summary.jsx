import React, { useState, useEffect } from "react";
import axios from "axios";
import handleError from "../Helpers/handleError";
import uuid from "react-uuid";

export default function Summary() {
  const [report, setReport] = useState([]);
  const [errors,setErrors] = useState([])
  const [dateValue, setDateValue] = useState()
  const [isRequesting, setRequesting] = useState(false)
  const [isLoaded, setLoaded] = useState(false)

  useEffect(() => {
    const getReports = async () => {
      setRequesting(true)
      await getResultSummary();
      setLoaded(true)
  
    };
    try{
        getReports();
    }
    catch(error){
        handleError(error, setErrors)
        setReport([])
    }
    
   
  }, []);

  const getResultSummary= async (value) => {
    const path = value?`/result/summary?date=${value}`: `/result/summary`
    const {data:{data}}= await axios.get(path);
    setReport((prev) => [...data]);
    setRequesting(false)
  };

  const change = (e) => {
    setReport([]);
    setRequesting(true);
    const target = e.target;
    const value = target.value;
    const dateString = new Date(value).toISOString().split("T")[0]
    setDateValue(dateString);
  };

  const handleSubmit = async (e) => {
    setErrors([]);
    setRequesting(true);
    e.preventDefault();
    try {
      await getResultSummary(dateValue) 
      setRequesting(false);
    
    } catch (error) {
      handleError(error, setErrors);
      setReport([]);
    }
  };
  
  const columns = report.length> 0 && Object.keys(report[0]).map(val => val.toUpperCase())
  const entries = report.length> 0 && report.map(singleReport => Object.values(singleReport) )
  const Today = new Date().toISOString().split("T")[0];
  

  return (
    <div>
    {isLoaded && <div>
      
     
      <form onSubmit={handleSubmit}>
        <label>
          Get Report for: 
        <input type="date" onChange={change}/>
        </label>
        <input type="submit" value="Enter" />
      </form>
      {report.length > 0 && 
      <div>
        <h3>Report Summary for {dateValue? dateValue : Today }</h3>
        <table>
          <thead>
          <tr>{columns.map(columnName => <th key={uuid()}>{columnName}</th>)}</tr>
          </thead>
          <tbody>
          {entries.map(singleEntry => {
            return <tr key={uuid()}>
              {singleEntry.map(singleCell => <td key={uuid()}>{singleCell}</td>)}
            </tr>
          })}
          </tbody>
        </table>
        </div>}
      {report.length < 1 && !isRequesting && (
        <div>
          <h4>No Report for {dateValue? dateValue : Today } </h4>
          <p>Reason: No Spaces Created</p>
        </div>
      )}
    </div>}
    </div>
  );
}
