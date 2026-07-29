const { S3Client } = require('@aws-sdk/client-s3');
const multer = require('multer');
const multerS3 = require('multer-s3');
const path = require('path');

const s3 = new S3Client({
    region: process.env.S3_REGION_NAME || 'ap-south-1',
    credentials: {
        accessKeyId: process.env.S3_ACCESS_KEY,
        secretAccessKey: process.env.S3_SECRET_KEY,
    },
    endpoint: `https://${process.env.S3_ENDPOINT_URL}`,
    forcePathStyle: false
});

// Initialize upload
const upload = multer({
    storage: multerS3({
        s3: s3,
        bucket: process.env.S3_BUCKET_NAME,
        acl: 'public-read',
        contentType: multerS3.AUTO_CONTENT_TYPE,
        key: function (req, file, cb) {
            cb(null, 'uploads/' + file.fieldname + '-' + Date.now() + path.extname(file.originalname));
        }
    }),
    limits: { fileSize: 50000000 }, // Increased to 50MB
    fileFilter: function(req, file, cb) {
        checkFileType(file, cb);
    }
});

// Check File Type
function checkFileType(file, cb) {
    // Allowed extensions including iPhone HEIC / HEIF
    const filetypes = /jpeg|jpg|png|pdf|doc|docx|heic|heif/;
    // Check extension
    const extname = filetypes.test(path.extname(file.originalname).toLowerCase());
    // Check mime (including iOS image/heic, image/heif, application/octet-stream)
    const mimeTypes = /jpeg|jpg|png|pdf|doc|docx|heic|heif|octet-stream/;
    const mimetype = mimeTypes.test(file.mimetype);

    if (mimetype || extname) {
        return cb(null, true);
    } else {
        cb(new Error('Error: Images (JPG, PNG, HEIC), PDFs, and Docs only!'));
    }
}

module.exports = upload;
