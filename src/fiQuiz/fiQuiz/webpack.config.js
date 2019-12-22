/// <binding ProjectOpened='Watch - Development' />
const path = require("path");
const webpack = require('webpack');
const bundleFileName = 'bundle';

module.exports = [
    {
        mode: "development",
        entry: ['./src/panel/index.js', './src/panel/scss/index.scss'],
        output: {
            filename: bundleFileName + '-panel.js',
            path: path.resolve(__dirname, 'wwwroot/dist')
        },
        plugins: [
            new webpack.ProvidePlugin({
                $: "jquery",
                jQuery: "jquery",
                "window.jQuery": "jquery"
            })
        ],
        module: {
            rules: [{
                test: /\.scss$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: bundleFileName + '-panel.css'
                        }
                    },
                    {
                        loader: 'extract-loader'
                    },
                    {
                        loader: "css-loader"
                    },
                    {
                        loader: "sass-loader"
                    }
                ]
            }]
        }
    },
    {
        mode: "development",
        entry: ['./src/index.js', './src/scss/index.scss'],
        output: {
            filename: bundleFileName + '.js',
            path: path.resolve(__dirname, 'wwwroot/dist')
        },
        module: {
            rules: [
                {
                    test: /\.scss$/,
                    use: [
                        {
                            loader: 'file-loader',
                            options: {
                                name: bundleFileName + '.css'
                            }
                        },
                        {
                            loader: 'extract-loader'
                        },
                        {
                            loader: "css-loader"
                        },
                        {
                            loader: "sass-loader"
                        }
                    ]
                }
            ]
        }
    },
    {
        mode: "development",
        entry: ['./src/quiz-app/index.js'],
        resolve: {
            extensions: ['*', '.js', '.jsx']
        },
        output: {
            filename: bundleFileName + '-quiz-app.js',
            path: path.resolve(__dirname, 'wwwroot/dist')
        },
        module: {
            rules: [
                {
                    test: /\.(js|jsx)$/,
                    exclude: /node_modules/,
                    use: ['babel-loader']
                },
                {
                    test: /\.scss$/,
                    exclude: /node_modules/,
                    use: ['style-loader', 'css-loader', 'sass-loader']
                },
                {
                    test: /\.svg$/,
                    exclude: /node_modules/,
                    loader: 'svg-inline-loader'
                }
            ]
        }
    }
];