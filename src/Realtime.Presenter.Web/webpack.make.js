const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { DefinePlugin } = require('webpack');

module.exports = function (env) {
    return {
        devtool: 'source-map',
        entry: {
            'index': path.resolve(__dirname, 'src', 'index.tsx')
        },
        output: {
            path: path.resolve(__dirname, 'dist'),
            filename: '[name].js',
            sourceMapFilename: '[file].map'
        },
        resolve: {
            extensions: ['.tsx', '.ts', '.jsx', '.js', '.scss', '.css']
        },
        module: {
            rules: [
                {
                    use: 'babel-loader',
                    test: /\.(ts|tsx)$/
                },
                {
                    use: ['style-loader', 'css-loader', 'sass-loader'],
                    test: /\.(css|scss)$/
                },
                {
                    use: 'file-loader',
                    test: /\.(png|svg|jpg|jpeg|gif)$/,
                }
            ]
        },
        plugins: [
            new HtmlWebpackPlugin({
                filename: 'index.html',
                template: path.resolve(__dirname, 'src', 'index.html'),
                inject: 'body'
            }),
            new DefinePlugin({
                FUNCTION_URL: isProd(env)
                    ? JSON.stringify('https://realtime-app-func.azurewebsites.net')
                    : JSON.stringify('http://localhost:7071')
            })
        ]
    };
}

function isProd(env) {
    return env === 'prod';
}
