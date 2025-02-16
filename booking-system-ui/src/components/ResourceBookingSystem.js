import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Table, Container, Card, Alert, Spinner } from 'react-bootstrap';
import './ResourceBookingSystem.css';

const ResourceBookingSystem = () => {
    const [resources, setResources] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [selectedResource, setSelectedResource] = useState(null);
    const [bookingData, setBookingData] = useState({
      dateFrom: '',
      dateTo: '',
      bookedQuantity: 1
    });
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState('');
  
    useEffect(() => {
      fetchResources();
    }, []);
  
    const fetchResources = async () => {
      try {
        setIsLoading(true);
        setError(null);
        const response = await fetch('https://localhost:7186/api/Resources');
        if (!response.ok) throw new Error('Failed to fetch resources');
        const data = await response.json();
        setResources(data);
      } catch (err) {
        setError('Failed to load resources. Please try again later.');
      } finally {
        setIsLoading(false);
      }
    };
  
    const handleShow = (resource) => {
      setSelectedResource(resource);
      setShowModal(true);
      setBookingData({
        dateFrom: '',
        dateTo: '',
        bookedQuantity: 1
      });
    };
  
    const handleClose = () => {
      setShowModal(false);
      setSelectedResource(null);
      setError(null);
      setSuccess('');
    };
  
    const handleInputChange = (e) => {
      const { name, value } = e.target;
      setBookingData(prev => ({
        ...prev,
        [name]: value
      }));
    };
  
    const handleSubmit = async (e) => {
      e.preventDefault();
      setIsLoading(true);
      setError(null);
      
      try {
        const response = await fetch('https://localhost:7186/api/Bookings', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            resourceId: selectedResource.id,
            ...bookingData,
            bookedQuantity: parseInt(bookingData.bookedQuantity)
          }),
        });
  
        if (!response.ok) throw new Error('Booking failed');
        
        setSuccess('Booking created successfully!');
        setTimeout(() => {
          handleClose();
          fetchResources(); // Refresh the resources list
        }, 1500);
      } catch (err) {
        setError('Failed to create booking. Please try again.');
      } finally {
        setIsLoading(false);
      }
    };
  
    if (isLoading && !showModal) {
      return (
        <Container className="d-flex justify-content-center align-items-center" style={{ minHeight: '100vh' }}>
          <Spinner animation="border" variant="primary" />
        </Container>
      );
    }
  
    return (
      <Container className="py-4">
        {error && !showModal && (
          <Alert variant="danger" className="mb-4">
            {error}
          </Alert>
        )}
  
        <Card className="mb-4">
          <Card.Header className="bg-primary text-white">
            <h2 className="mb-0">Resource Booking System</h2>
          </Card.Header>
          <Card.Body>
            <Table responsive striped bordered hover>
              <thead className="bg-light">
                <tr>
                  <th>ID</th>
                  <th>Name</th>
                  <th>Available Quantity</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {resources.map((resource) => (
                  <tr key={resource.id}>
                    <td>{resource.id}</td>
                    <td>{resource.name}</td>
                    <td>{resource.quantity}</td>
                    <td>
                      <Button 
                        variant="outline-primary" 
                        onClick={() => handleShow(resource)}
                        className="book-button"
                      >
                        Book Here
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Card.Body>
        </Card>
  
        <Modal show={showModal} onHide={handleClose}>
          <Modal.Header closeButton>
            <Modal.Title>
              Book Resource: {selectedResource?.name}
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            {error && <Alert variant="danger" className="mb-3">{error}</Alert>}
            {success && <Alert variant="success" className="mb-3">{success}</Alert>}
            
            <Form onSubmit={handleSubmit}>
              <Form.Group className="mb-3">
                <Form.Label>Date From</Form.Label>
                <Form.Control
                  type="datetime-local"
                  name="dateFrom"
                  value={bookingData.dateFrom}
                  onChange={handleInputChange}
                  required
                />
              </Form.Group>
  
              <Form.Group className="mb-3">
                <Form.Label>Date To</Form.Label>
                <Form.Control
                  type="datetime-local"
                  name="dateTo"
                  value={bookingData.dateTo}
                  onChange={handleInputChange}
                  required
                />
              </Form.Group>
  
              <Form.Group className="mb-3">
                <Form.Label>Quantity</Form.Label>
                <Form.Control
                  type="number"
                  name="bookedQuantity"
                  value={bookingData.bookedQuantity}
                  onChange={handleInputChange}
                  min="1"
                  max={selectedResource?.quantity}
                  required
                />
                <Form.Text className="text-muted">
                  Maximum available: {selectedResource?.quantity}
                </Form.Text>
              </Form.Group>
  
              <div className="d-flex justify-content-end gap-2">
                <Button variant="secondary" onClick={handleClose} disabled={isLoading}>
                  Cancel
                </Button>
                <Button variant="primary" type="submit" disabled={isLoading}>
                  {isLoading ? (
                    <>
                      <Spinner
                        as="span"
                        animation="border"
                        size="sm"
                        role="status"
                        aria-hidden="true"
                        className="me-2"
                      />
                      Booking...
                    </>
                  ) : (
                    'Book Now'
                  )}
                </Button>
              </div>
            </Form>
          </Modal.Body>
        </Modal>
      </Container>
    );
  };
  
  export default ResourceBookingSystem;