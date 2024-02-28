import './Homepage.css';
import { useEffect, useState } from 'react';

const getBusinessClients = async () => {
  const baseUrl = process.env.REACT_APP_BUSINESS_CLIENTS_MANAGER_URL;
  const resp = await fetch(`${baseUrl}/business-clients?from=0&to=100`);
  return resp;
};

const updatePostcodes = async () => {
  const requestOptions = {
    method: 'PUT'
  };
  const baseUrl = process.env.REACT_APP_BUSINESS_CLIENTS_MANAGER_URL;
  const resp = await fetch(`${baseUrl}/business-clients/postcode`, requestOptions);
  return resp;
};

function Homepage() {
  const [clients, setClients] = useState([]);
  const [updatedPostcodes, setUpdatedPostcodes] = useState(-1);
  const [clientFetchErr, setClientFetchErr] = useState("");
  const [updatePostcodesErr, setUpdatePostcodesErr] = useState("");

  useEffect(() => {
    const fetchClients = async () => {
      const resp = await getBusinessClients();
      if (resp.ok) {
        setClients(await resp.json());
      } else {
        setClientFetchErr(resp.statusText);
      }
    };
    fetchClients();
  }, []);

  const onUpdatePostcodesClick = async () => {
    const resp = await updatePostcodes();
    if (resp.ok) {
      const json = await resp.json();
      setClientFetchErr("");
      setUpdatedPostcodes(json.updatedItems);
    } else {
      setUpdatedPostcodes(-1);
      setUpdatePostcodesErr(resp.statusText);
    }
  };

  return (
    <div className="App">
      <div>
        <button onClick={onUpdatePostcodesClick}>Update postcodes</button>
      </div>
      <div>
        {updatedPostcodes >= 0 ? `Updated postcodes ${updatedPostcodes}` : null}
      </div>
      <div>
        {clientFetchErr ? `Updating postcodes failed ${updatePostcodesErr}` : null}
      </div>
      <div>
        {clientFetchErr ? `Fetching clients failed ${clientFetchErr}` : null}
      </div>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Address</th>
            <th>Postcode</th>
          </tr>
        </thead>
        <tbody>
          {clients.map((x,i) => {
            return (
              <tr key={i}>
                <td>{x.name}</td>
                <td>{x.address}</td>
                <td>{x.postcodeName}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}

export default Homepage;
