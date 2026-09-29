.class public Lcom/facebook/internal/logging/monitor/MonitorLog;
.super Ljava/lang/Object;
.source "MonitorLog.java"

# interfaces
.implements Lcom/facebook/internal/logging/ExternalLog;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/facebook/internal/logging/monitor/MonitorLog$LogBuilder;
    }
.end annotation


# static fields
.field private static final INVALID_TIME:I = -0x1

.field private static final serialVersionUID:J = 0x1L

.field private static validPerformanceEventNames:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private hashCode:I

.field private logEvent:Lcom/facebook/internal/logging/LogEvent;

.field private timeSpent:I

.field private timeStart:J


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 59
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    sput-object v0, Lcom/facebook/internal/logging/monitor/MonitorLog;->validPerformanceEventNames:Ljava/util/Set;

    .line 60
    sget-object v0, Lcom/facebook/internal/logging/monitor/MonitorLog;->validPerformanceEventNames:Ljava/util/Set;

    const-string v1, "FB_CORE_STARTUP"

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method public constructor <init>(Lcom/facebook/internal/logging/monitor/MonitorLog$LogBuilder;)V
    .locals 2

    .line 63
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 64
    invoke-static {p1}, Lcom/facebook/internal/logging/monitor/MonitorLog$LogBuilder;->access$000(Lcom/facebook/internal/logging/monitor/MonitorLog$LogBuilder;)Lcom/facebook/internal/logging/LogEvent;

    move-result-object v0

    iput-object v0, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    .line 65
    invoke-static {p1}, Lcom/facebook/internal/logging/monitor/MonitorLog$LogBuilder;->access$100(Lcom/facebook/internal/logging/monitor/MonitorLog$LogBuilder;)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    .line 66
    invoke-static {p1}, Lcom/facebook/internal/logging/monitor/MonitorLog$LogBuilder;->access$200(Lcom/facebook/internal/logging/monitor/MonitorLog$LogBuilder;)I

    move-result p1

    iput p1, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    return-void
.end method

.method static synthetic access$302(Lcom/facebook/internal/logging/monitor/MonitorLog;I)I
    .locals 0

    .line 45
    iput p1, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    return p1
.end method

.method static synthetic access$402(Lcom/facebook/internal/logging/monitor/MonitorLog;J)J
    .locals 0

    .line 45
    iput-wide p1, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    return-wide p1
.end method

.method static synthetic access$500()Ljava/util/Set;
    .locals 1

    .line 45
    sget-object v0, Lcom/facebook/internal/logging/monitor/MonitorLog;->validPerformanceEventNames:Ljava/util/Set;

    return-object v0
.end method


# virtual methods
.method public convertToJSONObject()Lorg/json/JSONObject;
    .locals 6

    .line 182
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    :try_start_0
    const-string v1, "event_name"

    .line 184
    iget-object v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    invoke-virtual {v2}, Lcom/facebook/internal/logging/LogEvent;->getEventName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "category"

    .line 185
    iget-object v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    invoke-virtual {v2}, Lcom/facebook/internal/logging/LogEvent;->getLogCategory()Lcom/facebook/internal/logging/LogCategory;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 187
    iget-wide v1, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    const-wide/16 v3, 0x0

    cmp-long v5, v1, v3

    if-eqz v5, :cond_0

    const-string v1, "time_start"

    .line 188
    iget-wide v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    invoke-virtual {v0, v1, v2, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;J)Lorg/json/JSONObject;

    .line 190
    :cond_0
    iget v1, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    if-eqz v1, :cond_1

    const-string v1, "time_spent"

    .line 191
    iget v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :cond_1
    return-object v0

    :catch_0
    const/4 v0, 0x0

    return-object v0
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 7
    .param p1    # Ljava/lang/Object;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    const/4 v1, 0x0

    if-eqz p1, :cond_3

    .line 169
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    if-eq v2, v3, :cond_1

    goto :goto_1

    .line 172
    :cond_1
    check-cast p1, Lcom/facebook/internal/logging/monitor/MonitorLog;

    .line 174
    iget-object v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    invoke-virtual {v2}, Lcom/facebook/internal/logging/LogEvent;->getEventName()Ljava/lang/String;

    move-result-object v2

    iget-object v3, p1, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    invoke-virtual {v3}, Lcom/facebook/internal/logging/LogEvent;->getEventName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    .line 175
    invoke-virtual {v2}, Lcom/facebook/internal/logging/LogEvent;->getLogCategory()Lcom/facebook/internal/logging/LogCategory;

    move-result-object v2

    iget-object v3, p1, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    invoke-virtual {v3}, Lcom/facebook/internal/logging/LogEvent;->getLogCategory()Lcom/facebook/internal/logging/LogCategory;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/facebook/internal/logging/LogCategory;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    iget-wide v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    iget-wide v4, p1, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    cmp-long v6, v2, v4

    if-nez v6, :cond_2

    iget v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    iget p1, p1, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    if-ne v2, p1, :cond_2

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    :goto_0
    return v0

    :cond_3
    :goto_1
    return v1
.end method

.method public getEventName()Ljava/lang/String;
    .locals 1

    .line 71
    iget-object v0, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    invoke-virtual {v0}, Lcom/facebook/internal/logging/LogEvent;->getEventName()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getLogCategory()Lcom/facebook/internal/logging/LogCategory;
    .locals 1

    .line 76
    iget-object v0, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    invoke-virtual {v0}, Lcom/facebook/internal/logging/LogEvent;->getLogCategory()Lcom/facebook/internal/logging/LogCategory;

    move-result-object v0

    return-object v0
.end method

.method public getTimeSpent()I
    .locals 1

    .line 84
    iget v0, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    return v0
.end method

.method public getTimeStart()J
    .locals 2

    .line 80
    iget-wide v0, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    return-wide v0
.end method

.method public hashCode()I
    .locals 6

    .line 154
    iget v0, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->hashCode:I

    if-nez v0, :cond_0

    const/16 v0, 0x20f

    .line 156
    iget-object v1, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    invoke-virtual {v1}, Ljava/lang/Object;->hashCode()I

    move-result v1

    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    .line 157
    iget-wide v1, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    iget-wide v3, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    const/16 v5, 0x20

    ushr-long/2addr v3, v5

    xor-long/2addr v1, v3

    long-to-int v1, v1

    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    .line 158
    iget v1, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    iget v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    ushr-int/2addr v2, v5

    xor-int/2addr v1, v2

    add-int/2addr v0, v1

    .line 159
    iput v0, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->hashCode:I

    .line 161
    :cond_0
    iget v0, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->hashCode:I

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 4

    const-string v0, ": %s"

    .line 141
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "event_name"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "category"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "time_start"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "time_spent"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x4

    new-array v1, v1, [Ljava/lang/Object;

    iget-object v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    .line 146
    invoke-virtual {v2}, Lcom/facebook/internal/logging/LogEvent;->getEventName()Ljava/lang/String;

    move-result-object v2

    const/4 v3, 0x0

    aput-object v2, v1, v3

    iget-object v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->logEvent:Lcom/facebook/internal/logging/LogEvent;

    .line 147
    invoke-virtual {v2}, Lcom/facebook/internal/logging/LogEvent;->getLogCategory()Lcom/facebook/internal/logging/LogCategory;

    move-result-object v2

    const/4 v3, 0x1

    aput-object v2, v1, v3

    iget-wide v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeStart:J

    .line 148
    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    const/4 v3, 0x2

    aput-object v2, v1, v3

    iget v2, p0, Lcom/facebook/internal/logging/monitor/MonitorLog;->timeSpent:I

    .line 149
    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    const/4 v3, 0x3

    aput-object v2, v1, v3

    .line 141
    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
