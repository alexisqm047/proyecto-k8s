{{/*
Nombre corto del chart.
*/}}
{{- define "cotizacion-service.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Nombre completo por defecto (release + nombre del chart).
Se trunca a 63 caracteres porque algunos campos de nombre en Kubernetes
tienen ese limite (norma DNS).
*/}}
{{- define "cotizacion-service.fullname" -}}
{{- if .Values.fullnameOverride }}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- $name := default .Chart.Name .Values.nameOverride }}
{{- if contains $name .Release.Name }}
{{- .Release.Name | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" }}
{{- end }}
{{- end }}
{{- end }}

{{/*
Nombre y version del chart, usado en la label "helm.sh/chart".
*/}}
{{- define "cotizacion-service.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Labels comunes que se agregan a todos los recursos del chart.
*/}}
{{- define "cotizacion-service.labels" -}}
helm.sh/chart: {{ include "cotizacion-service.chart" . }}
{{ include "cotizacion-service.selectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{/*
Labels usadas por el selector del Deployment/Service para encontrar los pods.
*/}}
{{- define "cotizacion-service.selectorLabels" -}}
app.kubernetes.io/name: {{ include "cotizacion-service.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}

{{/*
Nombre del ServiceAccount a usar (creado por el chart o uno existente).
*/}}
{{- define "cotizacion-service.serviceAccountName" -}}
{{- if .Values.serviceAccount.create }}
{{- default (include "cotizacion-service.fullname" .) .Values.serviceAccount.name }}
{{- else }}
{{- default "default" .Values.serviceAccount.name }}
{{- end }}
{{- end }}
