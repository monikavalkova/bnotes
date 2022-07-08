import React from 'react';
import PropTypes from 'prop-types';

function AddFriendForm({ addFriend }) {
  const handleSubmit = e => {
    e.preventDefault();
    const firstName = document.getElementById('nameFriendToAdd');
    const birthDate = document.getElementById('bdFriendToAdd');

    if (!firstName || !firstName.value || !birthDate || !birthDate.value) {
      console.log('First name and birthday are required.');
      return;
    }
    addFriend({ firstName: firstName.value, birthDate: birthDate.value });
    firstName.value = '';
    birthDate.value = '';
  }

  return (
    <form className="card-form"
      onSubmit={handleSubmit}>
      <div className="input">
        <input id="nameFriendToAdd" type="text" className="input-field" placeholder="Name" />
      </div>
      <div className="input">
        <input id="bdFriendToAdd" type="date" className="input-field" placeholder="BirthDate" />
      </div>

      <div className="action">
        <button id="btnFriendToAdd" type="submit" className="action-button">add</button>
      </div>
    </form>
  );
}

AddFriendForm.propTypes = {
  addFriend: PropTypes.func.isRequired,
};

export default AddFriendForm;