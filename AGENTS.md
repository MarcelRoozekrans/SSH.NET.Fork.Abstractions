# AGENTS.md: SSH.NET.Fork.Abstractions

A small `netstandard2.0` library that wraps the SFTP client of the `SSH.NET.Fork` package
(`Renci.SshNet`) behind interfaces, so consumers can mock it in tests. It ships as one NuGet package,
`SSH.NET.Fork.Abstractions`. There is no test project.

## Layout

- Everything lives in `src/SSH.NET.Fork.Abstractions/Sftp/`: `ISftpClient`, `ISftpFile` and
  `ISftpClientFactory`, plus their adapters `SftpClientAdapter`, `SftpFileAdapter` and
  `SftpClientFactory`. The interfaces mirror the public SFTP surface of `Renci.SshNet.SftpClient`; the
  adapters only delegate.
- The solution is `src/SSH.NET.Fork.Abstractions.sln` and holds that one project. `global.json` does
  not exist, so no SDK is pinned.
- `Directory.Build.props` only switches NuGetAudit on, to match what ci-baseline runs.

## Build and test

Prerequisite: a .NET SDK; the only target is `netstandard2.0`, which needs no runtime to build.
There are no tests to run, and `codecov.yml` has no coverage to read.

Release job, `appveyor.yml` (AppVeyor runs `build.ps1`, whose `dotnet` commands are):

```bash
dotnet restore src/SSH.NET.Fork.Abstractions.sln
dotnet build src/SSH.NET.Fork.Abstractions.sln -c Release -v q --no-restore /nologo
dotnet pack src/SSH.NET.Fork.Abstractions/SSH.NET.Fork.Abstractions.csproj -c Release -o artifacts --include-symbols --no-build --no-restore /nologo
```

`build.ps1` deletes `artifacts` first. The same three commands, run from the repo root, are the
inner loop (verified here with SDK 10.0).
GitHub Actions `baseline-checks.yml` calls the ci-baseline `baseline.yml@v1` with
`solution: src/SSH.NET.Fork.Abstractions.sln` (gitleaks, CodeQL, NuGetAudit); `history-scan.yml` is a
manual full-history secret scan.

## Conventions and gotchas

- Exceptions to the org rules below, because this repo does not use release-please:
  - Releases: there is no `release-please-config.json` and no release PR. A release is a git tag on
    `master`; AppVeyor then builds, packs and pushes the package to NuGet (`appveyor_repo_tag: true`,
    `branch: master`). Conventional Commit types do not decide anything here.
  - The package version is the hand-set `<Version>` in the csproj (`2018.8.25.2`). Change it in that
    one file before tagging; nothing bumps it for you.
  - The default branch is `master`, not `main`.
  - Test coverage: there are no tests and no coverage gate, so the 80% floor does not apply, and the
    `ci-baseline` coverage workflow is not used. Only the `baseline.yml` security checks run.
  - CI: the build and publish run on AppVeyor, not GitHub Actions.
- `TreatWarningsAsErrors` is on, with SonarAnalyzer.CSharp and Lindhart.Analyser.MissingAwaitWarning
  referenced. A new analyzer warning breaks the build and the release.
- `NoWarn` is `1701;1702;S3881` (S3881 is Sonar's IDisposable pattern rule). Do not add more
  suppressions; fix the code.
- Adding or changing a member means three places: the interface, the adapter that delegates to
  `Renci.SshNet`, and, for construction, `ISftpClientFactory` plus `SftpClientFactory`. The adapter
  must call the real `SftpClient` member of the same name.
- `LangVersion` is `latest`, but the target is `netstandard2.0`: stay with APIs that exist there.

<!-- org-core:start sha256=08a88996bc9165f888fc0260607cfd0670cae128c323e16707c6b452ee66222d (generated from docker-nuc scripts/agents-md/org-core-personal.md; do not edit here) -->
## Org rules

These rules hold in every public MarcelRoozekrans repo.

### Commits and releases

- Conventional Commits. release-please reads them on the default branch, and the changelog sections
  in `release-please-config.json` decide which types count. The standard: `feat`, `fix`, `perf`,
  `deps` and `revert` open or update the release PR; `docs`, `test`, `refactor`, `build`, `ci`,
  `chore` and `style` do not, unless the commit is breaking or has a `Release-As:` footer. The
  release happens when that PR merges. A breaking change needs `!` or a `BREAKING CHANGE:` footer.
- A commit body never contains nested parentheses. release-please cannot parse them and silently
  drops the whole commit from the changelog, while the workflow still reports success.
- After merging a commit that should release, check that it appears in the release-please PR.

### Branches and pull requests

- Every change goes through a branch and a pull request. Never push to the default branch directly.
- A PR body says what the diff does, and nothing the diff does not do.
- Fix review findings in the PR under review, including small ones. File an issue only for a finding
  that belongs to other code, needs a design decision, is its own piece of work, or waits on
  something external.

### CI

- The .NET repos call the shared workflows of `MarcelRoozekrans/ci-baseline`, pinned to `@v1`, never
  `@main`.
- Its coverage gate fails the build when line coverage drops below the floor of 80%, and Codecov
  fails a PR that lowers project coverage by more than 1 percentage point. New code comes with
  tests.
- A repo with a lower floor documents the reason and links the issue that raises it.
<!-- org-core:end -->
