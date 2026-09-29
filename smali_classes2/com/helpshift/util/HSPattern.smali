.class public final Lcom/helpshift/util/HSPattern;
.super Ljava/lang/Object;
.source "HSPattern.java"


# static fields
.field private static final EMAIL_LIMIT:I = 0x100

.field private static final IDENTIFIER_LIMIT:I = 0x2ee

.field private static customPropertyPattern:Ljava/util/regex/Pattern;

.field private static domainNamePattern:Ljava/util/regex/Pattern;

.field private static emailPattern:Ljava/util/regex/Pattern;

.field private static ipPattern:Ljava/util/regex/Pattern;

.field private static namePattern:Ljava/util/regex/Pattern;

.field private static positiveNumbersPattern:Ljava/util/regex/Pattern;

.field private static propertyKeyPattern:Ljava/util/regex/Pattern;

.field private static specialCharPattern:Ljava/util/regex/Pattern;

.field private static timeStampPattern:Ljava/util/regex/Pattern;

.field private static urlPattern:Ljava/util/regex/Pattern;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static checkIpv4Address(Ljava/lang/String;)Z
    .locals 5

    .line 117
    sget-object v0, Lcom/helpshift/util/HSPattern;->ipPattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "^(\\d+)\\.(\\d+)\\.(\\d+)\\.(\\d+)$"

    .line 118
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->ipPattern:Ljava/util/regex/Pattern;

    :cond_0
    const/4 v0, 0x0

    if-eqz p0, :cond_6

    .line 120
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    if-nez v1, :cond_1

    goto :goto_2

    .line 123
    :cond_1
    sget-object v1, Lcom/helpshift/util/HSPattern;->ipPattern:Ljava/util/regex/Pattern;

    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v1, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    .line 125
    invoke-virtual {p0}, Ljava/util/regex/Matcher;->matches()Z

    move-result v1

    if-eqz v1, :cond_5

    const/4 v1, 0x1

    const/4 v2, 0x1

    .line 126
    :goto_0
    invoke-virtual {p0}, Ljava/util/regex/Matcher;->groupCount()I

    move-result v3

    if-ge v2, v3, :cond_4

    .line 127
    invoke-virtual {p0, v2}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v3

    .line 128
    invoke-static {v3}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result v3

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    .line 129
    invoke-virtual {v3}, Ljava/lang/Integer;->intValue()I

    move-result v4

    if-ltz v4, :cond_3

    invoke-virtual {v3}, Ljava/lang/Integer;->intValue()I

    move-result v3

    const/16 v4, 0xff

    if-le v3, v4, :cond_2

    goto :goto_1

    :cond_2
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_3
    :goto_1
    return v0

    :cond_4
    return v1

    :cond_5
    return v0

    :cond_6
    :goto_2
    return v0
.end method

.method public static getComponentPlaceHolderPattern(Ljava/lang/String;)Ljava/util/regex/Pattern;
    .locals 2

    .line 140
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "^[\\p{L}\\p{N}-]+_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "_\\d{17}-[0-9a-z]{15}$"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object p0

    return-object p0
.end method

.method public static getDomainNamePattern()Ljava/util/regex/Pattern;
    .locals 1

    .line 151
    sget-object v0, Lcom/helpshift/util/HSPattern;->domainNamePattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "^[\\p{L}\\p{N}][\\p{L}\\p{N}-]*[\\p{L}\\p{N}]\\.helpshift\\.(com|mobi)$"

    .line 152
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->domainNamePattern:Ljava/util/regex/Pattern;

    .line 154
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSPattern;->domainNamePattern:Ljava/util/regex/Pattern;

    return-object v0
.end method

.method private static getEmailPattern()Ljava/util/regex/Pattern;
    .locals 1

    .line 187
    sget-object v0, Lcom/helpshift/util/HSPattern;->emailPattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "(?i)^[\\p{L}\\p{N}\\p{M}\\p{S}\\p{Po}A-Z0-9._%\'-]{1,64}(\\+.*)?@[\\p{L}\\p{M}\\p{N}\\p{S}A-Z0-9\'.-]{1,246}\\.[\\p{L}\\p{M}\\p{N}\\p{S}A-Z]{2,8}[^\\s]*$"

    .line 188
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->emailPattern:Ljava/util/regex/Pattern;

    .line 192
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSPattern;->emailPattern:Ljava/util/regex/Pattern;

    return-object v0
.end method

.method private static getPositiveNumbersPattern()Ljava/util/regex/Pattern;
    .locals 1

    .line 196
    sget-object v0, Lcom/helpshift/util/HSPattern;->positiveNumbersPattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "^[+]?\\p{N}+(\\.\\p{N}+)?$"

    .line 197
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->positiveNumbersPattern:Ljava/util/regex/Pattern;

    .line 200
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSPattern;->positiveNumbersPattern:Ljava/util/regex/Pattern;

    return-object v0
.end method

.method public static getPropertyKeyPattern()Ljava/util/regex/Pattern;
    .locals 1

    .line 158
    sget-object v0, Lcom/helpshift/util/HSPattern;->propertyKeyPattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "^[\\p{L}\\p{N}][\\p{L}\\p{N}\\p{Pd}\\p{Pc}]*[\\p{L}\\p{N}]$"

    .line 159
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->propertyKeyPattern:Ljava/util/regex/Pattern;

    .line 161
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSPattern;->propertyKeyPattern:Ljava/util/regex/Pattern;

    return-object v0
.end method

.method public static getTimeStampPattern()Ljava/util/regex/Pattern;
    .locals 1

    .line 144
    sget-object v0, Lcom/helpshift/util/HSPattern;->timeStampPattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "^\\d+.\\d{3}$"

    .line 145
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->timeStampPattern:Ljava/util/regex/Pattern;

    .line 147
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSPattern;->timeStampPattern:Ljava/util/regex/Pattern;

    return-object v0
.end method

.method public static getUrlPattern()Ljava/util/regex/Pattern;
    .locals 1

    .line 165
    sget-object v0, Lcom/helpshift/util/HSPattern;->urlPattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "[^\\p{Z}\\n\\p{Ps}]+://[^\\p{Z}\\n\\p{Pe}.]+(\\.[^\\p{Z}\\n\\p{Pe}.]+)*"

    .line 173
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->urlPattern:Ljava/util/regex/Pattern;

    .line 175
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSPattern;->urlPattern:Ljava/util/regex/Pattern;

    return-object v0
.end method

.method public static hasOnlySpecialCharacters(Ljava/lang/String;)Z
    .locals 1

    .line 95
    sget-object v0, Lcom/helpshift/util/HSPattern;->specialCharPattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "\\W+"

    .line 96
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->specialCharPattern:Ljava/util/regex/Pattern;

    .line 98
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSPattern;->specialCharPattern:Ljava/util/regex/Pattern;

    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    .line 100
    invoke-virtual {p0}, Ljava/util/regex/Matcher;->matches()Z

    move-result p0

    return p0
.end method

.method public static isPositiveNumber(Ljava/lang/String;)Z
    .locals 1

    .line 179
    invoke-static {p0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p0, 0x0

    return p0

    .line 183
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HSPattern;->getPositiveNumbersPattern()Ljava/util/regex/Pattern;

    move-result-object v0

    invoke-virtual {v0, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    invoke-virtual {p0}, Ljava/util/regex/Matcher;->matches()Z

    move-result p0

    return p0
.end method

.method public static isValidEmail(Ljava/lang/String;)Z
    .locals 1

    if-nez p0, :cond_0

    const/4 p0, 0x0

    return p0

    .line 28
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HSPattern;->getEmailPattern()Ljava/util/regex/Pattern;

    move-result-object v0

    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    .line 29
    invoke-virtual {p0}, Ljava/util/regex/Matcher;->matches()Z

    move-result p0

    return p0
.end method

.method public static isValidLoginEmail(Ljava/lang/String;)Z
    .locals 3

    if-eqz p0, :cond_3

    .line 75
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 79
    :cond_0
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    .line 80
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v1

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    const/4 v2, 0x0

    if-eq v1, v0, :cond_1

    return v2

    .line 85
    :cond_1
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    const/16 v1, 0x100

    if-le v0, v1, :cond_2

    return v2

    .line 89
    :cond_2
    invoke-static {}, Lcom/helpshift/util/HSPattern;->getEmailPattern()Ljava/util/regex/Pattern;

    move-result-object v0

    invoke-virtual {v0, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    .line 91
    invoke-virtual {p0}, Ljava/util/regex/Matcher;->matches()Z

    move-result p0

    return p0

    :cond_3
    :goto_0
    const/4 p0, 0x1

    return p0
.end method

.method public static isValidLoginIdentifier(Ljava/lang/String;)Z
    .locals 4

    const/4 v0, 0x1

    if-eqz p0, :cond_3

    .line 51
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v1

    if-nez v1, :cond_0

    goto :goto_1

    .line 54
    :cond_0
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v1

    .line 56
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v2

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    const/4 v3, 0x0

    if-eq v2, v1, :cond_1

    return v3

    .line 60
    :cond_1
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result p0

    const/16 v1, 0x2ee

    if-gt p0, v1, :cond_2

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    :goto_0
    return v0

    :cond_3
    :goto_1
    return v0
.end method

.method public static isValidName(Ljava/lang/String;)Z
    .locals 1

    .line 33
    sget-object v0, Lcom/helpshift/util/HSPattern;->namePattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "^[\\p{L}\\p{M}\\p{N}].*"

    .line 34
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->namePattern:Ljava/util/regex/Pattern;

    .line 36
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSPattern;->namePattern:Ljava/util/regex/Pattern;

    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    .line 37
    invoke-virtual {p0}, Ljava/util/regex/Matcher;->matches()Z

    move-result p0

    return p0
.end method

.method public static sanitiseCustomPropertyKey(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    .line 104
    sget-object v0, Lcom/helpshift/util/HSPattern;->customPropertyPattern:Ljava/util/regex/Pattern;

    if-nez v0, :cond_0

    const-string v0, "^[A-Za-z0-9_]+$"

    .line 105
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/util/HSPattern;->customPropertyPattern:Ljava/util/regex/Pattern;

    .line 107
    :cond_0
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    const-string v0, "-"

    const-string v1, "_"

    .line 108
    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p0

    const-string v0, " "

    const-string v1, "_"

    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p0

    .line 109
    sget-object v0, Lcom/helpshift/util/HSPattern;->customPropertyPattern:Ljava/util/regex/Pattern;

    invoke-virtual {v0, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v0

    .line 110
    invoke-virtual {v0}, Ljava/util/regex/Matcher;->matches()Z

    move-result v0

    if-nez v0, :cond_1

    const/4 p0, 0x0

    :cond_1
    return-object p0
.end method
