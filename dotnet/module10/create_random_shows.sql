DELETE TicketTypes
DELETE Shows
INSERT INTO Shows(Id, ArtistId, VenueId, DoorsOpen, ShowStart)
SELECT NEWID(), Artists.Id, Venues.Id,
       DATEADD(dd, ABS(CHECKSUM(NEWID()) % 180), '2023-01-01 19:00:00'),
       DATEADD(dd, ABS(CHECKSUM(NEWID()) % 180), '2023-01-01 19:00:00')
       FROM Artists CROSS JOIN Venues

UPDATE Shows SET ShowStart = DATEADD(mm, 90, DoorsOpen)

INSERT INTO TicketTypes(Id, ShowId, Name, Price)
SELECT NEWID(), Shows.Id, 'Standing',
       CASE CountryCode
        WHEN 'GB' then 10 + 2.5 * ABS(CHECKSUM(NEWID()) % 8)
        WHEN 'NO' then 100 + 10 * ABS(CHECKSUM(NEWID()) % 20)
        WHEN 'SE' then 120 + 10 * ABS(CHECKSUM(NEWID()) % 15)
        WHEN 'RS' then 500 + 50 * ABS(CHECKSUM(NEWID()) % 30)
        ELSE 12 + 2.5 * ABS(CHECKSUM(NEWID()) % 12)
    END
    FROM Shows
INNER JOIN Venues V on Shows.VenueId = V.Id

UNION

SELECT NEWID(), Shows.Id, 'Seated',
       CASE CountryCode
        WHEN 'GB' then 10 + 2.5 * ABS(CHECKSUM(NEWID()) % 8)
        WHEN 'NO' then 100 + 10 * ABS(CHECKSUM(NEWID()) % 20)
        WHEN 'SE' then 120 + 10 * ABS(CHECKSUM(NEWID()) % 15)
        WHEN 'RS' then 500 + 50 * ABS(CHECKSUM(NEWID()) % 30)
        ELSE 12 + 2.5 * ABS(CHECKSUM(NEWID()) % 12)
    END
    FROM Shows
INNER JOIN Venues V on Shows.VenueId = V.Id
WHERE CHECKSUM(VenueID) % 3 = 0

UNION

SELECT NEWID(), Shows.Id, 'VIP Meet & Greet',
       CASE CountryCode
        WHEN 'GB' then 50 + 5 * ABS(CHECKSUM(NEWID()) % 12)
        WHEN 'NO' then 500 + 100 * ABS(CHECKSUM(NEWID()) % 10)
        WHEN 'SE' then 600 + 100 * ABS(CHECKSUM(NEWID()) % 5)
        WHEN 'RS' then 2000 + 100 * ABS(CHECKSUM(NEWID()) % 20)
        ELSE 60 + 10 * ABS(CHECKSUM(NEWID()) % 6)
    END
    FROM Shows
INNER JOIN Venues V on Shows.VenueId = V.Id
WHERE CHECKSUM(Shows.Id) % 4 = 0
