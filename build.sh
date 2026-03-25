#!/bin/bash
set -e

echo "🏗️  Building and testing bnotes project..."
echo ""

# Build and test .NET API
echo "📦 Building .NET API..."
dotnet restore
dotnet build --no-restore --configuration Release

echo ""
echo "🧪 Testing .NET API..."
dotnet test --no-restore --configuration Release --verbosity normal

echo ""
# Build and test React UI
echo "📦 Building React UI..."
cd bnotes-ui
npm ci
npm test -- --watchAll=false --passWithNoTests
npm run build
cd ..

echo ""
echo "✅ All builds and tests completed successfully!"
