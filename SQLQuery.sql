select * from MsStorageLocations
select * from MsUsers
select * from TrBpkbs

INSERT INTO MsUsers VALUES 
('admin', 'admin123', 1),
('user1', 'password1', 1),
('user2', 'password2', 1);

INSERT INTO MsStorageLocations VALUES
('L001', 'Gudang Utama'),
('L002', 'Gudang Cabang A'),
('L003', 'Gudang Cabang B'),
('L004', 'Gudang Pusat'),
('L005', 'Gudang Selatan');

INSERT INTO TrBpkbs (AgreementNumber, BpkbNo, BranchId, BpkbDate, FakturNo, FakturDate, LocationId, PoliceNo, BpkbDateIn, CreatedBy, CreatedOn, LastUpdatedBy, LastUpdatedOn)
VALUES
('AGR001', 'BPKB001', 'BR001', '2024-01-01', 'FK001', '2024-01-10', 'L001', 'PL001', '2024-01-05', 'admin', GETDATE(), 'admin', GETDATE()),
('AGR002', 'BPKB002', 'BR002', '2024-02-01', 'FK002', '2024-02-10', 'L002', 'PL002', '2024-02-05', 'admin', GETDATE(), 'admin', GETDATE()),
('AGR003', 'BPKB003', 'BR003', '2024-03-01', 'FK003', '2024-03-10', 'L003', 'PL003', '2024-03-05', 'admin', GETDATE(), 'admin', GETDATE()),
('AGR004', 'BPKB004', 'BR004', '2024-04-01', 'FK004', '2024-04-10', 'L004', 'PL004', '2024-04-05', 'admin', GETDATE(), 'admin', GETDATE()),
('AGR005', 'BPKB005', 'BR005', '2024-05-01', 'FK005', '2024-05-10', 'L005', 'PL005', '2024-05-05', 'admin', GETDATE(), 'admin', GETDATE());